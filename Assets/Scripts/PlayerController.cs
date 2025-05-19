using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float _moveSpeed;
    public float jumpPower;

    private Vector2 InputsystemInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        // 이미 인풋시스템에서 w시 transformposition을 Vector2.(x, y) 값이 1씩 들어가게 세팅해둠.
        if (context.phase == InputActionPhase.Performed)
        {


        }
    }


}
