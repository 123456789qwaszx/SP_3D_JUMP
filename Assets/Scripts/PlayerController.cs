using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float _moveSpeed;
    public float jumpPower;

    void Start()
    {
        // 이벤트가 두번 호출되는 것 방지
        Managers.Char.KeyAction -= OnKeyboard;
        Managers.Char.KeyAction += OnKeyboard;
    }

    
    public void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * _moveSpeed;
        }
    }


}
