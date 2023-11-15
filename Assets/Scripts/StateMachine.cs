using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public GameObject prefabGamePiece;
    public GameObject prefabCell;
    public GameObject prefabPlayArea;

    private IGameState currentState = null;



    void Start()
    {
       StatePlay(); 
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
        SetState(new StatePlay(5, 5, 6, prefabGamePiece, prefabPlayArea, prefabCell));
    }

    public void StateTitle()
    {
    }

    public void StateGameOver()
    {
    }

}
