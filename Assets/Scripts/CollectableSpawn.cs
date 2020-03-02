using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawn : MonoBehaviour {
    public float spawnTime = 5.0f;
    public Treasure myTreasureClone;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 0,spawnTime);
	}

    void Spawn()
    {
        if(myTreasureClone == null)
        {
            Debug.Log("make coin");
            myTreasureClone = Instantiate(Resources.Load<Treasure>("Treasure"), this.transform.position, Quaternion.identity);
            myTreasureClone.myParentSpawn = this;
        }
    }
	
}
