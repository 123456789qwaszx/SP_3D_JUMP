using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAIT_RUNState : SceneLinkedSMB<PlayerController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_MonoBehaviour.CanAct = false;
        
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.CanAct = true;
    }
}
