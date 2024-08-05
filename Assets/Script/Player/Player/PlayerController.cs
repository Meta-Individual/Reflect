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

    public IPlayerState CurrentState { get; set; }

    public IPlayerState _idleState, _walkState, _waitState;

    private AudioSource _audioSrc;
    float moveX, moveY;
    bool isMoving = false;

    private void Start()
    {
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _waitState = gameObject.AddComponent<PlayerWaitState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSrc = GetComponent<AudioSource>();

        libraryKey = false;

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();

        moveX = Input.GetAxis("Horizontal") * walkSpeed;
        moveY = Input.GetAxis("Vertical") * walkSpeed;

        _rigidbody.velocity = new Vector2(moveX, moveY);

        isMoving = _rigidbody.velocity.magnitude > 0;

        if (isMoving)
        {
            if (!_audioSrc.isPlaying)
                _audioSrc.Play();
        }
        else
        {
            _audioSrc.Stop();
        }
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
}
