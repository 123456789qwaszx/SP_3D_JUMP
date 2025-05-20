using UnityEngine;
using UnityEngine.Animations;

// 크게 하는 역할은 없음
// 그냥 상태들만 가지고 있고, 
// 상태들에게 플레이어를 참조해줘야 하니까 참조 연결해주고
// Initialize해서 처음 상태를 가지고 있음. CurrentState
// 구현하기에 따라서 이전상태 등 여러가지를 다 가지고 있을 수 있음

// 다만 스테이트를 추가할때, 스테이트 변수만 추가해주고
// 실제 구현은 인터페이스를 구체화시킨 클래스에서 작성함.

//     public void Initialize(IPlayerState startingState)
//     {
//         CurrentState = startingState;
//         startingState.Enter();
//     }

//     public void TransitionTo(IPlayerState nextState)
//     {
//         CurrentState.Exit();
//         CurrentState = nextState;
//         nextState.Enter();
//     }


//     public void Update()
//     {
//         if (CurrentState != null)
//         {
//             CurrentState.Update();
//         }
//     }

// 이런 느낌.
// Iplayer를 유니티에서 제공해준 StateMachineBehaviour 이 녀석이 하는 중.

public class SceneLinkedSMB<T> : SealedSMB
    where T : MonoBehaviour
{
    protected T m_MonoBehaviour;

    bool m_FirstFrameHappened;
    bool m_LastFrameHappened;

    public static void Initialise(Animator animator, T monoBehaviour)
    {
        SceneLinkedSMB<T>[] sceneLinkedSMBs = animator.GetBehaviours<SceneLinkedSMB<T>>();

        for (int i = 0; i < sceneLinkedSMBs.Length; i++)
        {
            sceneLinkedSMBs[i].InternalInitialise(animator, monoBehaviour);
        }
    }

    protected void InternalInitialise(Animator animator, T monoBehaviour)
    {
        m_MonoBehaviour = monoBehaviour;
        OnStart(animator);
    }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        m_FirstFrameHappened = false;

        OnSLStateEnter(animator, stateInfo, layerIndex);
        OnSLStateEnter(animator, stateInfo, layerIndex, controller);
    }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (!animator.gameObject.activeSelf)
            return;

        if (animator.IsInTransition(layerIndex) && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex) && m_FirstFrameHappened)
        {
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && !m_LastFrameHappened && m_FirstFrameHappened)
        {
            m_LastFrameHappened = true;

            OnSLStatePreExit(animator, stateInfo, layerIndex);
            OnSLStatePreExit(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex) && !m_FirstFrameHappened)
        {
            m_FirstFrameHappened = true;

            OnSLStatePostEnter(animator, stateInfo, layerIndex);
            OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex, controller);
        }
    }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        m_LastFrameHappened = false;

        OnSLStateExit(animator, stateInfo, layerIndex);
        OnSLStateExit(animator, stateInfo, layerIndex, controller);
    }

    /// <summary>
    /// Called by a MonoBehaviour in the scene during its Start function.
    /// </summary>
    public virtual void OnStart(Animator animator) { }

    /// <summary>
    /// Called before Updates when execution of the state first starts (on transition to the state).
    /// </summary>
    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after OnSLStateEnter every frame during transition to the state.
    /// </summary>
    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called on the first frame after the transition to the state has finished.
    /// </summary>
    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called every frame after PostEnter when the state is not being transitioned to or from.
    /// </summary>
    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called on the first frame after the transition from the state has started.  Note that if the transition has a duration of less than a frame, this will not be called.
    /// </summary>
    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after OnSLStatePreExit every frame during transition to the state.
    /// </summary>
    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after Updates when execution of the state first finshes (after transition from the state).
    /// </summary>
    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called before Updates when execution of the state first starts (on transition to the state).
    /// </summary>
    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after OnSLStateEnter every frame during transition to the state.
    /// </summary>
    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called on the first frame after the transition to the state has finished.
    /// </summary>
    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called every frame when the state is not being transitioned to or from.
    /// </summary>
    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called on the first frame after the transition from the state has started.  Note that if the transition has a duration of less than a frame, this will not be called.
    /// </summary>
    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after OnSLStatePreExit every frame during transition to the state.
    /// </summary>
    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after Updates when execution of the state first finshes (after transition from the state).
    /// </summary>
    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }
}



public abstract class SealedSMB : StateMachineBehaviour
{
    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
}