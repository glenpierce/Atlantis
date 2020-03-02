using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    private static UIManager _instance;

    public UIScreen openingScreen;

	public UIScreen lobbyScreen;


    public UIScreen currentScreen;

    //We create a Dictionary so that with the Type of any screen (E.g. 'ExitPopup') we can access the instance of that screen
	private Dictionary<Type, UIScreen> screens;
    // Use this for initialization
    void Awake()
    {
        screens = new Dictionary<Type, UIScreen>();

        foreach (UIScreen screen in GetComponentsInChildren<UIScreen>())
        {
            //We deactivate every screen at the start
            screen.gameObject.SetActive(false);
            screens.Add(screen.GetType(), screen);
        }
        //We get the Type of the openingScreen that has been set in this inspector
        if (openingScreen != null)
        {
            Show(openingScreen.GetType());
        }
    }

    //We create the Generic method Show, which allows us to pass in a type as a parameter
    //We can only provide types that inherit from UIScreen (because of the CONSTRAINT T : UIScreen)
    public void Show<T> () where T : UIScreen
    {
        Type screenType = typeof(T);
        Show(screenType);
    }

    private void Show (Type screenType)
    {
        //The currentScreen is null in the beginning
        if (currentScreen != null)
        {
            //Deactivate the currentScreen
            currentScreen.gameObject.SetActive(false);
        }

        //We access the screens Dictionary by providing the Type as a key, which will return to us the instance of that screenType
        UIScreen newScreen = screens[screenType];
        newScreen.gameObject.SetActive(true);
        currentScreen = newScreen;
    }
		
}
