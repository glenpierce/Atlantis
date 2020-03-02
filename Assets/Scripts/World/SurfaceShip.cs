using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceShip : MonoBehaviour {
    public int totalScore = 0;
    public GameObject owner;
    private float radius = 10;
    private GameState gameState;
    private Animator playerAnimator;

    // Use this for initialization
    void Start () {
        // TODO: set position
        // TODO: assign player
        var gameStateContainer = GameObject.Find("GameStateContainer");
        gameState = gameStateContainer.GetComponent<GameStateManager>().gameState;
        playerAnimator = owner.GetComponentInChildren<Animator>();
    }

	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("onTriggerEnter SurfaceShip");
        if (col.GetComponent<Treasure>() && !col.GetComponent<Treasure>().deposited)
        {
            
            Treasure treasure = col.GetComponent<Treasure>();
            treasure.deposited = true;
            totalScore += treasure.value;
            Debug.Log("Score updated: "+ totalScore);
            treasure.transform.SetParent(null);
            owner.GetComponent<Player>().StopHolding();
            treasure.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            ((GameScreen)(UIManager.instance.currentScreen)).SetScore(totalScore, owner.GetComponent<Player>().PlayerNumber);
            //playerAnimator.SetBool("IsHolding", false);
            AudioSource[] audioSource = treasure.GetComponents<AudioSource>();
            audioSource[0].Play();
        }
    }
}
