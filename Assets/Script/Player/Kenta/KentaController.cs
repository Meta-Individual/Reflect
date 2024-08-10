﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KentaController : MonoBehaviour
{
    public Animator anim;

    public Transform[] waypoints;  // 이동할 경로 지점들
    public float moveSpeed = 2f;   // 이동 속도
    public float waitTime = 1f;    // 각 지점에서 대기 시간

    [HideInInspector]
    public PlayerController _playerController;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    private SpriteRenderer _spriteRenderer;


    [Header("Movement")]
    public float walkSpeed = 5f;    

    public IKentaState CurrentState
    {
        get; set;
    }

    public IKentaState _idleState, _walkState, _hideState, _outState;


    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _idleState = gameObject.AddComponent<KentaIdleState>();
        _walkState = gameObject.AddComponent<KentaWalkState>();
        _hideState = gameObject.AddComponent<KentaHideState>();
        _outState = gameObject.AddComponent<KentaOutState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentState = _hideState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IKentaState npcState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = npcState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void SetTransparency()
    {
        Color color = _spriteRenderer.color;

        color.a = 0f;

        _spriteRenderer.color = color;
    }

    public void RecoverTransparency()
    {
        Color color = _spriteRenderer.color;

        color.a = 1f;

        _spriteRenderer.color = color;
    }


    public void OnAnimationEnd()
    {
        // y축으로 -1만큼 이동
        transform.position += new Vector3(0, -3, 0);
    }
}
