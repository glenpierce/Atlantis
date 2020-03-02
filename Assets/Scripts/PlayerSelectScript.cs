using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectScript : MonoBehaviour
{


    string p1Label = "Player0";
    string p2Label = "Player1";
    string p3Label = "Player2";
    string p4Label = "Player3";

    string p1Enter = "";
    string p2Enter = "";
    string p3Enter = "";
    string p4Enter = "";

    string p1Exit = "";
    string p2Exit = "";
    string p3Exit = "";
    string p4Exit = "";

    string p1StartGame = "";
    string p2StartGame = "";
    string p3StartGame = "";
    string p4StartGame = "";

    List<bool> playerInGame;

    public GameState gameState;

    // Use this for initialization
    void Start()
    {
        playerInGame = new List<bool>();
        for(int i=0; i < 4; i++)
        {
            playerInGame.Add(false);
        }
        var gameStateContainer = GameObject.Find("GameStateContainer");
        gameState = gameStateContainer.GetComponent<GameStateManager>().gameState;

        Debug.Log("waiting for players to enter");
        p1Enter = p1Label + "AButton";
        p2Enter = p2Label + "AButton";
        p3Enter = p3Label + "AButton";
        p4Enter = p4Label + "AButton";

        p1Exit = p1Label + "BButton";
        p2Exit = p2Label + "BButton";
        p3Exit = p3Label + "BButton";
        p4Exit = p4Label + "BButton";

        p1StartGame = p1Label + "StartButton";
        p2StartGame = p2Label + "StartButton";
        p3StartGame = p3Label + "StartButton";
        p4StartGame = p4Label + "StartButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(p1Enter) && !playerInGame[0])
        {
            AddPlayer(1);
        }

        if (Input.GetButtonDown(p2Enter) && !playerInGame[1])
        {
            AddPlayer(2);
        }

        if (Input.GetButtonDown(p3Enter) && !playerInGame[2])
        {
            AddPlayer(3);
        }

        if (Input.GetButtonDown(p4Enter) && !playerInGame[3])
        {
            AddPlayer(4);
        }

        if (Input.GetButtonDown(p1Exit) && playerInGame[0])
        {
            removePlayer(1);
        }

        if (Input.GetButtonDown(p2Exit) && playerInGame[1])
        {
            removePlayer(2);
        }

        if (Input.GetButtonDown(p3Exit) && playerInGame[2])
        {
            removePlayer(3);
        }

        if (Input.GetButtonDown(p4Exit) && playerInGame[3])
        {
            removePlayer(4);
}

        //if (Input.GetButtonDown(p1Enter) && playerInGame[0])
        //{
        //    if (GameObject.Find("Sphere1").GetComponent<Rigidbody>().position.y < 0.55f)
        //    {
        //        GameObject.Find("Sphere1").GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));
        //    }
        //}

        //if (Input.GetButtonDown(p2Enter) && playerInGame[1])
        //{
        //    if (GameObject.Find("Sphere2").GetComponent<Rigidbody>().position.y < 0.55f)
        //    {
        //        GameObject.Find("Sphere2").GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));
        //    }
        //}

        //if (Input.GetButtonDown(p3Enter) && playerInGame[2])
        //{
        //    if (GameObject.Find("Sphere3").GetComponent<Rigidbody>().position.y < 0.55f)
        //    {
        //        GameObject.Find("Sphere3").GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));
        //    }
        //}

        //if (Input.GetButtonDown(p4Enter) && playerInGame[3])
        //{
        //    if (GameObject.Find("Sphere4").GetComponent<Rigidbody>().position.y < 0.55f)
        //    {
        //        GameObject.Find("Sphere4").GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));
        //    }
        //}
        if (Input.GetButtonDown(p1StartGame) && playerInGame[0])
        {
            LoadNextScene();
        }

        if (Input.GetButtonDown(p2StartGame) && playerInGame[1])
        {
            LoadNextScene();
        }

        if (Input.GetButtonDown(p3StartGame) && playerInGame[2])
        {
            LoadNextScene();

        }

        if (Input.GetButtonDown(p4StartGame) && playerInGame[3])
        {
            LoadNextScene();
        }
    }


    void LoadNextScene()
    {
        UIManager.instance.lobbyScreen.GetComponent<LobbyScreen>().OnReady();
        Debug.Log("LoadNextScene players: " + playerInGame[0] + playerInGame[1] + playerInGame[2] + playerInGame[3]);

        // establishing ownership of each SurfaceShips
        foreach (GameObject player in gameState.players)
        {
            GameObject surfaceShip = new GameObject();
            surfaceShip.AddComponent<SurfaceShip>();
            surfaceShip.GetComponent<SurfaceShip>().owner = player;
            gameState.surfaceShips.Add(surfaceShip);

        }

        SceneManager.LoadScene("SplitScreenSandbox", LoadSceneMode.Additive);
        Resources.UnloadUnusedAssets();
    }

    void AddPlayer(int playerNumberToBeAdded)
    {
        UIManager.instance.lobbyScreen.GetComponent<LobbyScreen>().OnAButtonPress(playerNumberToBeAdded, true);
        Debug.Log("Player "+ playerNumberToBeAdded + " enters game.");
        playerInGame[playerNumberToBeAdded - 1] = true;
        var playerGameObject = (GameObject)Instantiate(Resources.Load("Player"), new Vector3(0.0f,-100.0f,0.0f), Quaternion.identity);

        playerGameObject.tag = "Player" + (playerNumberToBeAdded - 1);
        DontDestroyOnLoad(playerGameObject);
        gameState.players.Add(playerGameObject);
        gameState.playerControllerMap.Add(gameState.players.Count - 1, playerNumberToBeAdded);
        GameObject sphere = GameObject.Find("Sphere" + playerNumberToBeAdded);
        var playerAnimator = sphere.transform.GetComponentInChildren<Animator>();
        playerAnimator.SetTrigger("Yay");
        //GameObject.Find("Sphere"+ playerNumberToBeAdded).AddComponent<Rigidbody>();

    }

    void removePlayer(int playerNumberToBeRemoved)
    {
        UIManager.instance.lobbyScreen.GetComponent<LobbyScreen>().OnAButtonPress(playerNumberToBeRemoved, false);
        Debug.Log("Player "+ playerNumberToBeRemoved + " leaves game.");
        playerInGame[playerNumberToBeRemoved - 1] = false;
        //Destroy(GameObject.Find("Sphere"+ playerNumberToBeRemoved).GetComponent<Rigidbody>());
        GameObject.Find("Sphere"+ playerNumberToBeRemoved).GetComponentInChildren<Animator>().SetTrigger("Yay");

        foreach (int j in gameState.playerControllerMap.Keys)
        {
            if (gameState.playerControllerMap[j] == playerNumberToBeRemoved)
            {
                Debug.Log("removing "+ gameState.playerControllerMap.Count + " " + gameState.playerControllerMap[j]+ " " + playerNumberToBeRemoved + " " + j + gameState.players.Count);
                gameState.players.RemoveAt(j);
                GameObject player = GameObject.FindGameObjectWithTag("Player" + (gameState.playerControllerMap[j]-1));
                Destroy(player);
                gameState.playerControllerMap.Remove(j);
                break;
            }

        }
    }
}
