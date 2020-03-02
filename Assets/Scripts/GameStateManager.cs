using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public GameState gameState;
	// Use this for initialization
	void Start () {
        Debug.Log("GameStateManager start");
        gameState = new GameState();
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
