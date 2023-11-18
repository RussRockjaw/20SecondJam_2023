using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMainMenu : IGameState
{
    private GameObject prefabMenu;

    private MainMenu menu;
    private StateMachine sm;


    public StateMainMenu(StateMachine s, GameObject g)
    {
        prefabMenu = g;
        sm = s;
    }
    public void Initialize()
    {
        menu = GameObject.Instantiate(prefabMenu).GetComponent<MainMenu>();
        menu.Setup();
        menu.startButton.onClick.AddListener(StartButton);
    }

    public void Cleanup()
    {
        GameObject.Destroy(menu.gameObject);
    }

    public void StartButton()
    {
        sm.StatePlay();
    }

    public void HandleUpdate()
    {

    }
}
