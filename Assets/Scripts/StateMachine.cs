using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public int defaultTime = 20;
    public int defaultPlayAreaW = 3;
    public int defaultPlayAreaH = 3;
    public int defaultMaxPieceSize = 5;

    public GameObject prefabGamePiece;
    public GameObject prefabCell;
    public GameObject prefabPlayArea;

    public GameObject prefabMainMenu;
    public GameObject prefabOptions;

    private IGameState currentState = null;
    private Settings defaultSettings;
    private Settings gameSettings;


    void Awake()
    {
        defaultSettings = new Settings(defaultTime, defaultPlayAreaW, defaultPlayAreaH, defaultMaxPieceSize);
        gameSettings = defaultSettings.Copy();
        StateTitle();
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
        SetState(new StatePlay(this, gameSettings, prefabGamePiece, prefabPlayArea, prefabCell));
    }

    public void StateTitle()
    {
        SetState(new StateMainMenu(this, prefabMainMenu));
    }

    public void StateGameOver()
    {
    }

    public void StateOptions()
    {
        SetState(new StateOptions(this, prefabOptions, defaultSettings));
    }

    public void SetGameSettings(Settings s)
    {
        gameSettings = s;
    }

    public void SetDefaultGameSettings()
    {
        gameSettings = defaultSettings.Copy();
    }
}
