using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public int playAreaW = 5;
    public int playAreaH = 5;
    public int maxPieceSize = 5;

    public GameObject prefabGamePiece;
    public GameObject prefabCell;
    public GameObject prefabPlayArea;

    private IGameState currentState = null;

    private GameObject stateMainMenu;
    private GameObject statePlay;
    private GameObject stateOptions;
    private GameObject stateGameOver;


    void Awake()
    {
        stateMainMenu = GameObject.Find("State_MainMenu");
        stateOptions = GameObject.Find("State_Options");
        statePlay = GameObject.Find("State_Play");
        stateGameOver = GameObject.Find("State_GameOver");

        stateMainMenu.SetActive(true);
        stateOptions.SetActive(false);
        statePlay.SetActive(false);
        stateGameOver.SetActive(false);
    }

    void Update()
    {
        if(currentState != null)
            currentState.HandleUpdate();
    }

    private void SetState(IGameState s)
    {
        if(currentState != null)
            currentState.Cleanup();

        currentState = s;
        currentState.Initialize();
    }

    public void StatePlay()
    {
        SetState(new StatePlay(playAreaW, playAreaH, maxPieceSize, prefabGamePiece, prefabPlayArea, prefabCell));
        stateMainMenu.SetActive(false);
        stateOptions.SetActive(false);
        statePlay.SetActive(true);
        stateGameOver.SetActive(false);

    }

    public void StateTitle()
    {
        stateMainMenu.SetActive(false);
        stateOptions.SetActive(false);
        statePlay.SetActive(true);
        stateGameOver.SetActive(false);
    }

    public void StateGameOver()
    {
        stateMainMenu.SetActive(false);
        stateOptions.SetActive(false);
        statePlay.SetActive(false);
        stateGameOver.SetActive(true);

    }

}
