using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    Animator animator;
    Camera _camera;

    public float _moveSpeed;
    public float jumpPower;
    public float smoothness = 10f;

    private float cur_wait_run_ratio;


    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;

        Managers.Char.KeyAction -= KeyboardAnim;
        Managers.Char.KeyAction += KeyboardAnim;
    }


    void Update()
    {
        InputKeyAction();
    }


    void LateUpdate()
    {
        Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);

    }



    void InputKeyAction()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        transform.position += moveDirection.normalized * _moveSpeed * Time.deltaTime;
    }


    // 좌표이동x 애니메이션만 실행. 아마 벽에 부딪혔을 때에도 실행이 되면 좋을 것 같아.
    public void KeyboardAnim()
    {

        if (Input.GetKey(KeyCode.W))
        {
            cur_wait_run_ratio = Mathf.Lerp(cur_wait_run_ratio, 1, 10.0f * Time.deltaTime);
            animator.SetFloat("wait_run_ratio", cur_wait_run_ratio);
            animator.Play("WAIT_RUN");
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.Play("JUMP");
        }
        else
        {
            cur_wait_run_ratio = Mathf.Lerp(cur_wait_run_ratio, 0, 3.0f * Time.deltaTime);
            animator.SetFloat("wait_run_ratio", cur_wait_run_ratio);
            animator.Play("WAIT_RUN");
        }
    }// -> 이렇게 하나하나 구현하기보다는 KeyboardMove 중일 때 특정애니메이션 실행. 이렇게 묶는게 좋을 듯. 어차피 바꿀거니 W만 만들어서 테스트해보자.
     //또 키입력 후, 입력을 멈추면 Idle로 초기화를 시키고 싶은데...


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
}
