using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float interactDistance = 2f; // ��ȣ�ۿ� ������ �Ÿ�
    public LayerMask interactableLayer; // ��ȣ�ۿ� ������ ���̾� ����
    public Vector2 interactionAreaSize = new Vector2(2f, 1f); // ��ȣ�ۿ� ������ ũ��


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
        Vector2 centerPosition = new(0.0f, 0.0f);
        // X������ �̵����� ���� �̵��ϴ� ���⿡ ���� ����, ������ ���� ����
        if (CurrentDirection.x != 0)
        {
            Vector2 location = new(anim.GetFloat("DirX"), 0.35f);
            interactionAreaSize = new(2.5f, 2.0f);
            centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        }
        //Y������ �̵����� ���� �̵��ϴ� ���⿡ ���� ��, �Ʒ� ���� ����
        else if(CurrentDirection.y != 0)
        {
            Vector2 location = new(0.0f, anim.GetFloat("DirY"));
            interactionAreaSize = new(5.5f, 1.5f);
            centerPosition = (Vector2)transform.position + CurrentDirection * (interactionAreaSize);
        }
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(centerPosition, interactionAreaSize, 0f, interactableLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("��ȣ�ۿ� ���: " + hitCollider.gameObject.name);
                break;
            }
        }
    }

    // ����׸� ���� ����� �׸��� (�����Ϳ����� ǥ�õ�)
    private void OnDrawGizmosSelected()
    {
        Vector2 location = new(0.0f, 1f);
        Gizmos.color = Color.yellow;
        Vector2 centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
    }
}
