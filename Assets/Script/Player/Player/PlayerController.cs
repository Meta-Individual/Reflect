using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameObject monologuePanel;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public bool libraryKey;

    public float interactDistance = 2f; // 상호작용 가능한 거리
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 설정


    [Header("Movement")]
    public float walkSpeed = 5f;    

    public IPlayerState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get;set;
    }

    public IPlayerState _idleState, _walkState, _waitState, _monoState;


    private void Start()
    {
        monologuePanel.SetActive(false);

        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _monoState = gameObject.AddComponent<PlayerMonologueState>();

        _rigidbody = GetComponent<Rigidbody2D>();

        libraryKey = false;

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
        CurrentDirection = new(anim.GetFloat("DirX"), anim.GetFloat("DirY"));
    }

    public void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, CurrentDirection, interactDistance, interactableLayer);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
