using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController _controller;
    Camera _camera;

    public float _moveSpeed = 5f;
    public float _jumpPower = 10f;
    public float _rotationSensitivety = 100f;

    private float cur_wait_run_ratio;


    public bool p_IsGrounded = true;
    public bool p_ReadyToJump;

    public float gravity = 15f;
    protected float VerticalSpeed;

    // const값은 정확한 계산법을 모르면 조절X gravity와 jumpPower값에만 변화를 줄 것
    const float StickingGravityProportion = 0.3f;
    const float JumpAbortSpeed = 1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    void Start()
    {
    }


    void FixedUpdate()
    {
        Move();
        Jump();
    }


    public void Move()
    {
        Vector3 dir = GameManager.Instance.MouseDir;
        Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
        moveDir = (Quaternion.Euler(0, _camera.transform.rotation.y, 0) * moveDir).normalized;

        if (moveDir != Vector3.zero)
        {
            _controller.Move(moveDir * Time.deltaTime * _moveSpeed);
            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotationSensitivety);

            cur_wait_run_ratio = Mathf.Lerp(cur_wait_run_ratio, 1, 10.0f * Time.deltaTime);
            animator.SetFloat("wait_run_ratio", cur_wait_run_ratio);
            animator.Play("WAIT_RUN");
        }
        else
        {
            cur_wait_run_ratio = Mathf.Lerp(cur_wait_run_ratio, 0, 3.0f * Time.deltaTime);
            animator.SetFloat("wait_run_ratio", cur_wait_run_ratio);
            animator.Play("WAIT_RUN");
        }
    }


    public void Jump()
    {
        // 혹시 2단점프나, 공중에서 점프아이템 먹는 기능을 위해 p_ReadyToJump체크도 추가함.
        // 실제 체크는 IsGround로만 해도됨.
        if (!GameManager.Instance.JumpInput && _controller.isGrounded)
            p_ReadyToJump = true;

        if (p_IsGrounded && p_ReadyToJump)
        {
            VerticalSpeed = -gravity * StickingGravityProportion;
            animator.Play("JUMP");

            if (GameManager.Instance.JumpInput && p_ReadyToJump)
            {
                VerticalSpeed = _jumpPower;
                p_IsGrounded = false;
                p_ReadyToJump = false;
            }
        }
        else
        {
            if (!GameManager.Instance.JumpInput && VerticalSpeed > 0.0f)
            {
                VerticalSpeed -= JumpAbortSpeed * Time.deltaTime;
            }

            // 공중에서 최고점을 터치하고, 추락하는 역할도 담당하지만,
            // 그것보다도 실제적인 중력의 작용을 겸함.
            // 이동하다 캐릭터가 공중에 조금씩 뜨는걸, ground로 밀어서 _controller.isGrounded를 true 만드는 역할도 등...
            VerticalSpeed -= gravity * Time.deltaTime;

            _controller.Move(VerticalSpeed * Vector3.up * Time.deltaTime);
        }

        //다시 점프 뛸 준비
        p_IsGrounded = _controller.isGrounded;
    }

}