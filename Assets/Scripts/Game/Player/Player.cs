using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController _playercontroller;
    protected Animator animator;

    private void Awake()
    {
        Managers.Char.Player = this;
        _playercontroller = GetComponent<PlayerController>();
    }

    protected void Onable()
    {
        SceneLinkedSMB<Player>.Initialise(animator, this);       
    }

}
