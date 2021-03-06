﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitPopup : UIScreen {

	public void OnYesButton ()
    {
        SceneManager.UnloadScene("SplitScreenSandbox");
        UIManager.instance.Show<MenuScreen>();
    }

    public void OnNoButton ()
    {
        UIManager.instance.Show<GameScreen>();
    }

    void OnEnable ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
