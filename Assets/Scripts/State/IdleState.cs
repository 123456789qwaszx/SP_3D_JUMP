using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    // 이건 소유하고 있는게 아니라 참조만 하는 것
    // 어떤 플레이어의 컨트롤인지 알아야함.
    private PlayerController player;

    public IdleState(PlayerController player){ this.player = player; }

    public void Update()
    {
        
    }
}

public interface IState
{
    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}