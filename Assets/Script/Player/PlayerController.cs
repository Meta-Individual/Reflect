using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;


    [Header("Movement")]
    public float walkSpeed = 5f;    

    public IPlayerState CurrentState
    {
        get; set;
    }

    public IPlayerState _idleState, _walkState;


    private void Start()
    {
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();

        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IPlayerState playerState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = playerState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public CharacterController GetCharacterController()
    {
        return _characterController;
    }

}
