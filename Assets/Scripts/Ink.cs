using UnityEngine;

public class Ink : MonoBehaviour
{
    private ParticleSystem _particleSystem;

	public void Start ()
	{
	    _particleSystem = gameObject.GetComponent<ParticleSystem>();
	}
	
	public void Update () {
	    if (!_particleSystem.IsAlive())
	    {
	        Destroy(gameObject);
	    }
	}
}
