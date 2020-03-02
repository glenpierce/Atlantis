using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour {


	public GameObject fishPrefab;
	public static int minTankSize = -100;
	public static int maxTankSize = 100;
	static int numFish = 100;
	public static GameObject[] fishClones = new GameObject[numFish];
	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < numFish; i++) {
			Vector3 pos = new Vector3 (Random.Range (minTankSize, maxTankSize),
				Random.Range (minTankSize, maxTankSize),
				Random.Range (minTankSize, maxTankSize));
			fishClones [i] = Instantiate (fishPrefab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0, 100) < 50) {
			 goalPos = new Vector3 (Random.Range (minTankSize, maxTankSize),
				Random.Range (minTankSize, maxTankSize),
				Random.Range (minTankSize, maxTankSize));
		}
	}
}
