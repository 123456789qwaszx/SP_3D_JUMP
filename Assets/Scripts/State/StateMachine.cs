using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public WalkState walkState;
    public JumpState junpState;
    public IdleState idleState;

    public StateMachine(PlayerController player)
    {
        this.walkState = new WalkState(player);
        this.jumpState = new JumpState(player);
        this.idleState = new IdleState(player);
    }


    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }


    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}

