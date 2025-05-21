using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 태그를 붙여서 가져올 수 있고,
    // 유니티를 다른 매니저에서 소유하게 한 후, 정보를 가져 올 수 있다.
    [SerializeField]
    private Vector3 _delta;

    [SerializeField]
    private GameObject _player;

    void Start()
    {
        _player = Managers.Char.Player;
    }

    void Update()
    {
        //transform.position = _player
    }

}
