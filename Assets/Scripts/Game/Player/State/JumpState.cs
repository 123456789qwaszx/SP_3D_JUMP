using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : SceneLinkedSMB<PlayerBehaviour>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.OnJump();

        //m_MonoBehaviour.Sound_Jump();
        
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
