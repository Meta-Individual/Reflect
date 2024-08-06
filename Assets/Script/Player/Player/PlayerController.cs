using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public bool libraryKey;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    public IPlayerState CurrentState { get; set; }

    public IPlayerState _idleState, _walkState, _waitState, _runState;

    public AudioSource _audioSrc;

    private void Start()
    {
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _runState = gameObject.AddComponent<PlayerRunState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSrc = GetComponent<AudioSource>();

        libraryKey = false;

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
        HandleRunInput();
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

    // public void Interact()
    // {
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
    //     if (hit.collider != null)
    //     {
    //         IInteractable interactable = hit.collider.GetComponent<IInteractable>();
    //         if (interactable != null)
    //         {
    //             interactable.Interact();
    //         }
    //     }
    // }

    private void HandleRunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0))
        {
            if (CurrentState == _walkState)
                ChangeState(_runState);
        }
        else
        {
            if (CurrentState == _runState)
                ChangeState(_walkState);
        }
    }
}
