using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRotation : MonoBehaviour
{
    Vector2 leftClickPoint;
    Vector2 middleClickPoint;

    [SerializeField]
    GameObject _looktarget;

    public float zoomSpeed = 5.0f;
    public float dragSpeed = 30.0f;

    void Start()
    {
        //_looktarget = Managers.Char.Player;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) leftClickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonDown(2)) middleClickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButton(0))
        {
            Vector3 m_dist = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - leftClickPoint);

            //m_dist.z = m_dist.y;
            m_dist.z = 0;
            m_dist.y = 0;

            Vector3 toDest = m_dist * (Time.deltaTime * dragSpeed);

            // y축 고정. 계산하기전에 미리 값을 저장해서, 0을 가지고 있는다. 
            float y = transform.position.y;

            transform.Translate(toDest);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            transform.LookAt(_looktarget.transform);
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 m_dist = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - middleClickPoint);

            m_dist.x = 0;
            m_dist.z = 0;

            Vector3 toDest = m_dist * (Time.deltaTime * dragSpeed);

            float x = transform.position.x;
            float z = transform.position.z;

            transform.Translate(toDest);
            transform.position = new Vector3(x, transform.position.y, z);

            transform.LookAt(_looktarget.transform);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            transform.position = _looktarget.transform.position + new Vector3(0.0f, 3.6f, -3.2f);
        }
        else
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float distance = transform.position.z - scrollWheel * zoomSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, distance);

            transform.LookAt(_looktarget.transform);
        }
    }
}

