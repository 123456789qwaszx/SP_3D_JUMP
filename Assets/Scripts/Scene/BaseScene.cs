using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
	void Awake()
	{
		Init();
	}

    void Update()
    {
        
    }

	protected void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        Resources.Load<EventSystem>("Prefabs").name = "@EventSystem";
    }
}
