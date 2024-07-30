using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Animator anim;

    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public bool isArrive = false;
    [HideInInspector]
    private SpriteRenderer _spriteRenderer;
    [HideInInspector]
    public ChairAndDeskMoving chair_desk_Move;


    [Header("Destination")]
    public List<Vector2> targetPositions;


    [Header("Movement")]
    public float walkSpeed = 5f;    

    public INPCState CurrentState
    {
        get; set;
    }

    public INPCState _idleState, _walkState, _hideState, _outState;


    private void Start()
    {
        _idleState = gameObject.AddComponent<NPCIdleState>();
        _walkState = gameObject.AddComponent<NPCWalkState>();
        _hideState = gameObject.AddComponent<NPCHideState>();
        _outState = gameObject.AddComponent<NPCOutOfDeskState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        chair_desk_Move = GetComponent<ChairAndDeskMoving>();

        CurrentState = _idleState;
        if(gameObject.name == "Kanna_kid")
        {
            CurrentState = _hideState;
        }
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(INPCState npcState)
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


    public bool Hide()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return true;
        }
        return false;
    }

    public bool OutOfDesk()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        return false;
    }

    public void OnAnimationEnd()
    {
        // y축으로 -1만큼 이동
        transform.position += new Vector3(0, -3, 0);
    }
}
