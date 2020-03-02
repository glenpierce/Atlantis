using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScreen : UIScreen {

	public Text player1;
	public Text player2;
	public Text player3;
	public Text player4;

	string readyMessage = "Ready";
	string initMessage;


	// Use this for initialization
	void Start () {
		initMessage = player1.text;
	}

	/// <summary>
	/// Changes the text in UI.
	/// Example use
	/// UIManager.instance.lobbyScreen.GetComponent<LobbyScreen> ().OnAButtonPress (1, true);
	/// </summary>
	/// <param name="number">Number.</param>
	public void OnAButtonPress (int number, bool ready)
	{
		switch(number){
		case 1:
			if (ready)
				player1.text = readyMessage;
			else
				player1.text = initMessage;
			break;
		case 2:
			if (ready)
				player2.text = readyMessage;
			else
				player2.text = initMessage;
			break;
		case 3:
			if (ready)
				player3.text = readyMessage;
			else
				player3.text = initMessage;
			break;
		case 4:
			if (ready)
				player4.text = readyMessage;
			else
				player4.text = initMessage;
			break;

		}
	}
		
	public void OnReady(){
		SceneManager.UnloadScene("Lobby");
		UIManager.instance.Show<GameScreen>();
	}

}
