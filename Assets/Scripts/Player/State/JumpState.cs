using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : SceneLinkedSMB<PlayerBehaviour>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play("JUMP_SQUASH");
        animator.Play("JUMP_STRETCH");
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
