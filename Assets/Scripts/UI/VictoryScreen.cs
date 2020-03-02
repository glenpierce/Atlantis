using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : UIScreen {
	public Text VictoryPlayer;
	int maxScore = 0;
	int playerNumber = 0;
	void OnEnable(){
		var playerSpaceShips = GameObject.Find ("GameStateContainer").GetComponent<GameStateManager> ().gameState.surfaceShips;
		for (int i = 0; i < playerSpaceShips.Count; i++) {
			Debug.Log ("score " + playerSpaceShips [i].gameObject.GetComponent<SurfaceShip> ().totalScore);
			if (playerSpaceShips [i].gameObject.GetComponent<SurfaceShip> ().totalScore > maxScore) {
				maxScore = playerSpaceShips [i].gameObject.GetComponent<SurfaceShip> ().totalScore;
				playerNumber = i;

			}
		}

		VictoryPlayer.text = "Player " + playerNumber + " Won " + "Score " + maxScore;
	}

	public void OnYesButton ()
	{
		UIManager.instance.Show<MenuScreen>();
		SceneManager.UnloadScene("SplitScreenSandbox");
		SceneManager.UnloadScene("DontDestroyOnLoad");

	}

}
