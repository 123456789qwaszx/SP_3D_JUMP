using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // 태그를 붙여서 가져올 수 있고,
    // 유니티를 다른 매니저에서 소유하게 한 후, 정보를 가져 올 수 있다.
    private Vector3 _delta = new Vector3(0.0f, 3.6f, -3.2f);

    [SerializeField]
    private GameObject _player;

    void Start()
    {
        _player = Managers.Char.Player;
    }
    void Update()
    {
        CameraLook();
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
        {
            float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
            transform.position = _player.transform.position + _delta.normalized * dist;
        }
        else
        {
            transform.position = _player.transform.position + _delta;
        }
    }

    

    public void rotatecamera()
    {
        // 마우스
        camCurXRot += mouseDelta.y * lookSensitvity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        camCurYRot += mouseDelta.x * lookSensitvity;
        camCurYRot = Mathf.Clamp(camCurYRot, minYLook, maxYLook);

        CameraContainer.transform.localEulerAngles = new Vector3(-camCurXRot, camCurYRot, 0);

        CameraContainer.transform.eulerAngles += new Vector3(mouseDelta.y * lookSensitvity, mouseDelta.x * lookSensitvity, 0);

    }

    public GameObject CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;

    public float minYLook;
    public float maxYLook;
    private float camCurYRot;

    public float lookSensitvity;
    private Vector2 mouseDelta;

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitvity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        camCurYRot += mouseDelta.x * lookSensitvity;
        camCurYRot = Mathf.Clamp(camCurYRot, minYLook, maxYLook);

        CameraContainer.transform.localEulerAngles = new Vector3(-camCurXRot, camCurYRot, 0);

        CameraContainer.transform.eulerAngles += new Vector3(mouseDelta.y * lookSensitvity, mouseDelta.x * lookSensitvity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }





    // public float checkRate = 0.05f;
    // private float lastChectTime;

    // public GameObject curInteractGameObject;

    // public void InteractionCheckItem()
    // {
    //     if (Time.time - lastChectTime > checkRate)
    //     {
    //         lastChectTime = Time.time;

    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;


    //         if (Input.GetMouseButtonDown(2))
    //         {

    //             Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

    //             if (Physics.Raycast(ray, out hit, 100.0f))
    //             {
    //                 Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
    //             }
    //         }
    //     }
    // }
}


            // if (Input.GetMouseButtonDown(0))
            // {
            //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //     Vector3 dir = mousePos - Camera.main.transform.position;
            //     dir = dir.normalized;

            //     Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

            //     RaycastHit hit;
            //     if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            //     {
            //         Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
            //     }
            // }
