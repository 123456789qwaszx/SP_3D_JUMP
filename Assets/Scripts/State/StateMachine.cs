using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 크게 하는 역할은 없음
// 그냥 상태들만 가지고 있고, 
// 상태들에게 플레이어를 참조해줘야 하니까 참조 연결해주고
// Initialize해서 처음 상태를 가지고 있음. CurrentState
// 구현하기에 따라서 이전상태 등 여러가지를 다 가지고 있을 수 있음

// 다만 스테이트를 추가할때, 스테이트 변수만 추가해주고
// 실제 구현은 인터페이스를 구체화시킨 클래스에서 작성함.
[Serializable]
public class StateMachine
{
    public IPlayerState CurrentState { get; private set; }

    public WalkState walkState;
    public JumpState jumpState;
    public IdleState idleState;

    public StateMachine(PlayerController player)
    {
        this.walkState = new WalkState(player);
        this.jumpState = new JumpState(player);
        this.idleState = new IdleState(player);
    }


    public void Initialize(IPlayerState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IPlayerState nextState)
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

