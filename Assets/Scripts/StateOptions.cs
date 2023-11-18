using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOptions : IGameState
{
    private GameObject prefabOptions;
    private Options options;
    private StateMachine sm;

    public StateOptions(StateMachine s, GameObject obj)
    {
       prefabOptions = obj;
       sm = s;
    }

    public void Initialize()
    {
        
    }

    public void Cleanup()
    {

    }

    public void HandleUpdate()
    {

    }
}
