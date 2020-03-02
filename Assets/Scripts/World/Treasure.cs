using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
    public int value = 0;
    public int weight = 0;
    public CollectableSpawn myParentSpawn;
    public bool deposited = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        var player = col.GetComponent<Player>();
        
        if (player != null) {
            myParentSpawn.myTreasureClone = null;
            player.animator.SetBool("IsHolding", true);
            Debug.Log("take coin");
            this.transform.SetParent(col.gameObject.transform);
            this.transform.position = player.transform.position + new Vector3(-1, -1, 0);
            AudioSource[] audioSource = this.GetComponents<AudioSource>();
            audioSource[0].Play();
        }
       
    }
}
