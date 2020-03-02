using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

	public float speed = 0.001f;
	public float rotationSpeed = 4.0f;

	Vector3 averageHeading;
	Vector3 averagePosition;

	float neighbourDistance = 3.0f;

	bool turning = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range (0.5f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

		float boundry = Vector3.Distance (transform.position, Vector3.zero);
		if(boundry >= FishManager.maxTankSize || boundry <= FishManager.minTankSize)
			turning = true;
		else
			turning = false;

		if (turning) {
			Vector3 direction = Vector3.zero - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction),
				rotationSpeed * Time.deltaTime);

			speed = Random.Range (0.5f, 1f);
		}
		else if(Random.Range (0, 5) < 1)
			ApplyRules ();

		transform.Translate (0, 0, Time.deltaTime * speed * 20);
	}

	void ApplyRules(){
		GameObject[] gos;
		gos = FishManager.fishClones;

		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;

		float gSpeed = 0.1f;

		Vector3 goalPos = FishManager.goalPos;

		float dist;

		int groupSize = 0;

		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);

				if (dist <= neighbourDistance) {
					vcenter += go.transform.position;
					groupSize++;

					if (dist < 1.0f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					Flock anotherFlock = go.GetComponent<Flock> ();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0) {
			vcenter = vcenter / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vcenter + vavoid) - transform.position;

			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);
			}
		}

	}
}
