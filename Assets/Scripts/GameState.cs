using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    public List<GameObject> players;
    public List<GameObject> surfaceShips;
    public Dictionary<int, int> playerControllerMap;
    public Dictionary<GameObject, GameObject> playerSpaceShipMap;

    public GameState()
    {
        Debug.Log("GameState constructor");
        this.players = new List<GameObject>();
        this.playerControllerMap = new Dictionary<int, int>();
        this.surfaceShips = new List<GameObject>();
    }

}
