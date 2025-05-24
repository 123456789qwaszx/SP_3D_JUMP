using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    protected CharacterController characterController;      // Reference used to actually move Player.
    protected Animator animator;                 // Reference used to make decisions based on Player's current animation and to set parameters.

    [Header("Audio")]
    public RandomAudioPlayer idleAudio;

    public LayerMask groundLayerMask;
    public Rigidbody _rigidbody;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Managers.Char.OnUpdate();
    }

    protected void Onable()
    {
        SceneLinkedSMB<PlayerBehaviour>.Initialise(animator, this);
    }


    public void Sound_Mumble()
    {
        if (idleAudio != null)
            idleAudio.PlayClip();
    }
    
    public void Sound_Jump()
    {
        if (idleAudio != null)
        idleAudio.PlayClip();
    }


}
