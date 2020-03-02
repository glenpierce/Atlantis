using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public float health = 4f;

	public void OnHit(float attackPower){
		if (health > 0) {
			health -= attackPower;
		}
	}

	void Update(){
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

}
