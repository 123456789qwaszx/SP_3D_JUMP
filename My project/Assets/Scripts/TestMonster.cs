using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestMonster : MonoBehaviour
{
    void Awake()
    {
        Monster monster = Monster.Create()
        .SetPosition(Vector3.right * 3.5f)
        .SetScale(Vector3.one * 3.5f)
        .AddEquipment<WoodenSword>();
    }
    

}
