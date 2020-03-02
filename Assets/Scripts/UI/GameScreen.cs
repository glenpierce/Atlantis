using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameScreen : UIScreen {

    public List<Text> score;

	public Text timeText;

	float time = 300f;
	// Use this for initialization
	void Start () {
        score.Add(GameObject.Find("Player1Text").GetComponent<Text>());
        score.Add(GameObject.Find("Player2Text").GetComponent<Text>());
        score.Add(GameObject.Find("Player3Text").GetComponent<Text>());
        score.Add(GameObject.Find("Player4Text").GetComponent<Text>());
    }

    void OnEnable ()
    {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
        {	
            UIManager.instance.Show<ExitPopup>();
        }

		time -= Time.deltaTime;

		timeText.text = "" + Mathf.FloorToInt(time);

		if (time <= 0) {
			
			UIManager.instance.Show<VictoryScreen>();
			SceneManager.UnloadScene("SplitScreenSandbox");
			SceneManager.UnloadScene("DontDestroyOnLoad");
		}
	}

    public void SetScore(int newScore, int playerNumber)
    {
        score[playerNumber].text = "Score: " + newScore;
    }
}
