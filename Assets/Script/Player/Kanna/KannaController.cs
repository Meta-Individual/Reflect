using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KannaController : MonoBehaviour
{
    public Animator anim;

    public Transform[] waypoints;  // 이동할 경로 지점들
    public Transform moveOut; //칸나가 나갈 현관 위치
    public float moveSpeed = 15f;   // 이동 속도
    public float waitTime = 1f;    // 각 지점에서 대기 시간
    [HideInInspector]
    public bool goToLivingRoom = false; // 칸나가 서재에서 거실로 이동하기 위한 변수
    [HideInInspector]
    public PlayerController _playerController;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    private SpriteRenderer _spriteRenderer;
    [HideInInspector]
    public ChairAndDeskMoving chair_desk_Move;

    [Header("Movement")]
    public float walkSpeed = 5f;

    [Header("Sound")]
    public AudioClip hideAndSeek; // 방문 여는 사운드

    public IKannaState CurrentState
    {
        get; set;
    }

    public IKannaState _idleState, _walkState, _hideState, _outState;


    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _idleState = gameObject.AddComponent<KannaIdleState>();
        _walkState = gameObject.AddComponent<KannaWalkState>();
        _hideState = gameObject.AddComponent<KannaHideState>();
        _outState = gameObject.AddComponent<KannaOutOfDeskState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        chair_desk_Move = GetComponent<ChairAndDeskMoving>();

        CurrentState = _hideState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IKannaState npcState)
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

    public void MoveKanna()
    {
        StartCoroutine(MoveOutKanna());
    }

    IEnumerator MoveOutKanna() // 김신을 거실까지 이동시키고 플레이어의 방향을 아래로 변환 후 대사 출력
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveOut.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveOut.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        this.gameObject.SetActive(false);
    }
}
