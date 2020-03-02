using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public abstract class BaseAI : BaseActor {

	public float minWaypointDistance = 1f;
	public float attackRange;
	public float attackPower;
	public float attackInterval;

	private IEnumerator currentState;

	protected Goal currentTarget;

	private Path currentPath;
	private int waypointIndex;

	private Seeker seeker;

	// Use this for initialization
	void Start () {
		
		seeker = GetComponent<Seeker>();

		SetState(OnIdle());
	}

	void SetState (IEnumerator newState)
	{
		//We ensure that the previous coroutine state is no longer running
		if (currentState != null)
		{
			StopCoroutine(currentState);
		}
		//We store the currently running Coroutine as a variable
		currentState = newState;
		//We start the coroutine
		StartCoroutine(currentState);
	}


	IEnumerator OnIdle ()
	{
		while (currentTarget == null)
		{
			FindGoal();
			yield return null;
		}
		//The target may have been set, but sometimes it takes a while for the path to calculate
		while(currentPath == null)
		{
			yield return null;
		}

		SetState(OnMoving());
	}

	IEnumerator OnMoving ()
	{
		waypointIndex = 0;
		//As long as the target != null (it could be destroyed), keep on moving towards it
		while (currentTarget != null)
		{
			if (waypointIndex < currentPath.vectorPath.Count)
			{
				Vector3 targetPos = currentPath.vectorPath[waypointIndex];
				//We have to normalize the direction vector (giving it a length of 1), because otherwise, the unit will move faster on long distances
				Vector3 direction = (targetPos - transform.position).normalized;
				//We multiply our direction by Time.deltaTime to ensure our game runs framerate independent
				transform.Translate(direction * Time.deltaTime * movementSpeed, Space.World);

				//We can't calculate a lookRotation on Vector3.zero. Just ensuring that the warning message does not pop up
				if (direction != Vector3.zero)
				{
					Quaternion lookRotation = Quaternion.LookRotation(direction);
					transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
				}

				if (Vector3.Distance(transform.position, targetPos) < minWaypointDistance)
				{
					waypointIndex++;
				}


				//If I am closer to the Goal than the sum of my range
				if (Vector3.Distance(transform.position, currentTarget.transform.position) < attackRange ) {
					//Attack Or Collects
					SetState(OnAttacking());
				}
			}
			yield return null;
		}
		//If target becomes null while walking towards it, we go back to Idle
		SetState(OnIdle());
	}

	IEnumerator OnAttacking ()
	{
		float timer = 0;
		while (currentTarget.health > 0)
		{
			timer += Time.deltaTime;
			if (timer >= attackInterval)
			{
				timer = 0;
				Attack();
			}
			yield return null;
		}
		//Make sure to 'forget' our currentTarget before we go back to Idle
		currentTarget = null;
		SetState(OnIdle());
	}
		
	protected abstract void Attack();

	void FindGoal(){

		Goal []allGoals = FindObjectsOfType<Goal> ();
		//If there are no goals left, we can't find any
		if (allGoals.Length == 0)
			return;

		Goal closestGoal = allGoals.OrderBy(b => Vector3.Distance(b.transform.position, transform.position)).
			First();

		currentTarget = closestGoal;
		
		currentPath = null;
		seeker.StartPath(transform.position, currentTarget.transform.position, OnPathComplete);
	}

	void OnPathComplete (Path newPath)
	{
		currentPath = newPath;
	}
}
