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
    public float _jumpPower = 5f;
    public float _rotationSensitivety = 100f;

    private float cur_wait_run_ratio;


    public bool p_IsGrounded = true;
    public bool p_ReadyToJump;

    public float gravity = 10f;
    protected float VerticalSpeed;


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


    void Update()
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
        if (!GameManager.Instance.JumpInput && _controller.isGrounded)
            p_ReadyToJump = true;

        if (p_IsGrounded)
        {
            VerticalSpeed = -gravity * StickingGravityProportion;

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
                Debug.Log("감속중");
            }

            VerticalSpeed -= gravity * Time.deltaTime;

            _controller.Move(VerticalSpeed * Vector3.up * Time.deltaTime);
        }

        p_IsGrounded = _controller.isGrounded;
    }

}