using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState;
    public void Initialize(EnemyState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
