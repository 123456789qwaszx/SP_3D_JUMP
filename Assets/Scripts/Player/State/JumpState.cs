using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : SceneLinkedSMB<PlayerController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.CanAct = false;
        //점프 이펙트 추가
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_MonoBehaviour.CanAct = true;
        // 점프 이펙트 삭제
        // ex) m_MonoBehaviour.effectObject.SetActive(false);
    }

}
