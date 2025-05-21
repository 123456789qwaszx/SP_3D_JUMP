using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SceneLinkedSMB<PlayerBehaviour>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.CalculateVerticalMovement();

        m_MonoBehaviour.Sound_Mumble();

    }
    
}