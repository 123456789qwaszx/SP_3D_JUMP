using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Condition class와 Player class간의 격을 깎지 않고 서로의 데이터를 주고 받는 일종의 전령
public class UICondition : MonoBehaviour
{
    public Condition health;

    void Start()
    {
        Managers.Char.Player.condition.uiCondition = this;
    }
}
