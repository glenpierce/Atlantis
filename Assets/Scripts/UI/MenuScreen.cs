using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreen : UIScreen {

	private string []pLabel;

	public void OnStartButton ()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
		UIManager.instance.Show<LobbyScreen>();
    }

	void Start(){
		pLabel = new string[4];
		pLabel [0] = "Player0";
		pLabel [1] = "Player1";
		pLabel [2] = "Player2";
		pLabel [3] = "Player3";
	}

	void Update(){

		foreach(string label in pLabel){
			if(Input.GetButtonDown(label + "AButton")){
				OnStartButton ();
			}
		}
	}
}
