using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenSetup : MonoBehaviour {

    SpawnPoint[] spawnPoints;
	// Use this for initialization
	void Start () {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
        GameObject gameStateContainer = GameObject.Find("GameStateContainer");
        GameState gameState;
        if(gameStateContainer == null)
        {
            Debug.LogError("Start from main Menu scene");
            //gameStateContainer = (GameObject)Instantiate(Resources.Load("GameStateContainer"));
            //GameStateManager gameStateManager = gameStateContainer.AddComponent<GameStateManager>();
            //Debug.LogWarning("No gameState found. Creating 4 player camera context");
            //gameState = gameStateManager.gameState;

            //for (int i = 0; i < 4; i++)
            //{
            //    var playerGameObject = (GameObject)Instantiate(Resources.Load("Player"));
            //    var position = (i - 2) * 5f;
            //    playerGameObject.transform.position = new Vector3(position, 0f, 0f);
            //    var player = playerGameObject.AddComponent<Player>();
            //    player.gameState = gameState;
            //    player.PlayerNumber = i;

            //    gameState.players.Add(playerGameObject);

            //}
        }

            
        gameState = gameStateContainer.GetComponent<GameStateManager>().gameState;
        Debug.Log("Loading Player Models" + gameState.players.Count);
        
        for (int i = 0; i < gameState.players.Count; i++)
        {
            int mappedPlayerNumber = gameState.playerControllerMap[i];
            GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player"+(mappedPlayerNumber-1));
            GameObject playerGameObject = playerGameObjects[0];
            Player player = playerGameObject.AddComponent<Player>();
            player.gameState = gameState;
            player.PlayerNumber = i;
        }
 

        for (int i = 0; i < gameState.players.Count; i++)
        {
            var playerCameraScript = gameState.players[i].AddComponent<SplitScreenCamera>();
            var player = gameState.players[i].GetComponent<Player>();
            
            var cameraGameObject = playerCameraScript.AddPlayerCamera(i, gameState.players[i], gameState.players.Count);
            cameraGameObject.transform.position = gameState.players[i].transform.position + new Vector3(0.0f, 1.0f, -3.0f);
            cameraGameObject.transform.rotation = gameState.players[i].transform.rotation;
            cameraGameObject.transform.SetParent(gameState.players[i].transform);
            gameState.players[i].transform.position = spawnPoints[i].transform.position;
            SurfaceShip surfaceShip = spawnPoints[i].gameObject.AddComponent<SurfaceShip>();
            surfaceShip.owner = player.gameObject;
            gameState.players[i].transform.LookAt(Vector3.zero);

        }
    }

    public void Init (GameState gState)
    {

    }
	
	// Update is called once per frame
	void Update () {
    }

}
