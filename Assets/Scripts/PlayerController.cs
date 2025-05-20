using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float _moveSpeed;
    public float jumpPower;
    public Rigidbody _rigidbody;
    private Animator animator;

    void Start()
    {
        // 이벤트가 두번 호출되는 것 방지
        Managers.Char.KeyAction -= KeyboardMove;
        Managers.Char.KeyAction += KeyboardMove;
        
        Managers.Char.KeyAction -= KeyboardAnim;
        Managers.Char.KeyAction += KeyboardAnim;

        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    // 좌표이동x 애니메이션만 실행. 아마 벽에 부딪혔을 때에도 실행이 되면 좋을 것 같아.
    public void KeyboardAnim()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.Play("RUN");
        }
        else
        {
            animator.Play("WAIT");
        }
    }// -> 이렇게 하나하나 구현하기보다는 KeyboardMove 중일 때 특정애니메이션 실행. 이렇게 묶는게 좋을 듯. 어차피 바꿀거니 W만 만들어서 테스트해보자.

    // 실제로 좌표가 이동
    public void KeyboardMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _moveSpeed;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }


}
