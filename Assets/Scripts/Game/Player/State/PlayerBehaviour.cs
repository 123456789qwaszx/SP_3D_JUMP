using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    protected CharacterController characterController;      // Reference used to actually move Player.
    protected Animator animator;                 // Reference used to make decisions based on Player's current animation and to set parameters.

    protected float jumpPower;
    protected bool isGrounded = true;            // Whether or not Player is currently standing on the ground.   
    protected bool readyToJump = true;           // Whether or not the input state and Player are correct to allow jumping.
    protected float verticalSpeed;               // How fast Player is currently moving up or down.

    protected float forwardSpeed;                // How fast Player is currently going along the ground.


    public float maxForwardSpeed = 8f;           // How fast Player can run.
    public float gravity = 20f;                  // How fast Player accelerates downwards when airborne.
    public float jumpSpeed = 10f;                // How fast Player takes off when jumping.


    const float StickingGravityProportion = 0.3f;
    const float JumpAbortSpeed = 10f;

    [Header("Audio")]
    public RandomAudioPlayer idleAudio;

    public LayerMask groundLayerMask;
    public Rigidbody _rigidbody;



    private void Awake()
    {
        Managers.Char.Player = gameObject;
        
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        Managers.Char.OnUpdate();
        CalculateVerticalMovement();
    }

    protected void Onable()
    {
        SceneLinkedSMB<PlayerBehaviour>.Initialise(animator, this);
    }


    public void OnJump()
    {
        
        _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        
    }


    // Called each physics step.
    public void CalculateVerticalMovement()
    {
        // If jump is not currently held and Player is on the ground then she is ready to jump.
        if (!Input.GetKey(KeyCode.Space) && isGrounded)
            readyToJump = true;

        if (Input.GetKey(KeyCode.Space))
        {
            // When grounded we apply a slight negative vertical speed to make Player "stick" to the ground.
            verticalSpeed = -gravity * StickingGravityProportion;

            // If jump is held, Player is ready to jump and not currently in the middle of a melee combo...
            if (Input.GetKey(KeyCode.Space) && readyToJump)
            {
                // ... then override the previously set vertical speed and make sure Player cannot jump again.
                verticalSpeed = jumpSpeed;
                isGrounded = false;
                readyToJump = false;
            }
        }
        else
        {
            // If Player is airborne, the jump button is not held and Player is currently moving upwards...
            if (!Input.GetKey(KeyCode.Space) && verticalSpeed > 0.0f)
            {
                // ... decrease Player's vertical speed.
                // This is what causes holding jump to jump higher that tapping jump.
                verticalSpeed -= JumpAbortSpeed * Time.deltaTime;
            }

            // If a jump is approximately peaking, make it absolute.
            if (Mathf.Approximately(verticalSpeed, 0f))
            {
                verticalSpeed = 0f;
            }

            // If Player is airborne, apply gravity.
            verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    // Called each physics step (so long as the Animator component is set to Animate Physics) after FixedUpdate to override root motion.
    public void OnAnimatorMove()
    {
        Vector3 movement;

        //If Player is on the ground...
        if (isGrounded)
        {
            //... raycast into the ground...
            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);

            if (Physics.Raycast(ray, out hit, 1.0f, groundLayerMask))
            {
                // ... and get the movement of the root motion rotated to lie along the plane of the ground.
                // animator.deltaPosition은 애니메이션에서 디자인한 실제 캐릭터 움직임을 반환함. 따라서 제자리걷기 시, 0을 반환함
                movement = Vector3.ProjectOnPlane(animator.deltaPosition, hit.normal);
            }
            else
            {
                // If no ground is hit just get the movement as the root motion.
                // Theoretically this should rarely happen as when grounded the ray should always hit.
                movement = animator.deltaPosition;
            }
        }

        else
        {
            // If not grounded the movement is just in the forward direction.
            movement = forwardSpeed * transform.forward * Time.deltaTime;
        }
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
