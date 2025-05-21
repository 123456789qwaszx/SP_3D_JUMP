using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRotation : MonoBehaviour
{
    [SerializeField]
    GameObject _looktarget;
    Vector2 clickPoint;

    private Vector3 _delta = new Vector3(0.0f, 3.6f, -3.2f);
    public float rotateSpeed = 500.0f;
    public float scrollSpeed = 2000.0f;
    private float xRotate_dist;
    private float yRotate_dist;

    float dragSpeed = 30.0f;

    bool isAlt;

    void Start()
    {
        //_player = Managers.Char.Player;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) clickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButton(0))
        {
            Vector3 m_dist = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
            //좌우, 상화로 움직이는 걸 -> 좌우, 앞뒤 이동으로 변경

            m_dist.z = 0;
            m_dist.y = 0;

            Vector3 toDest = m_dist * (Time.deltaTime * dragSpeed);

            // y축 고정. 계산하기전에 미리 값을 저장해서, 0을 가지고 있는다. 
            float y = transform.position.y;
            
            transform.Translate(toDest);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            transform.LookAt(_looktarget.transform);
        }
        else
        {

            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            Vector3 camDir = this.transform.localRotation * Vector3.forward;

            this.transform.position += camDir * Time.deltaTime * scrollWheel * scrollSpeed;
        }
    }
}

