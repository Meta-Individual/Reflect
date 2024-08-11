using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    [Header("Dialogue")]
    public GameObject monologuePanel; //독백 패널
    public GameObject dialoguePanel; //대화 패널
    public TMP_Text characterName; //대화 패널의 캐릭터 이름
    public TMP_Text dialogue; //대화 패널의 대사
    public Image charcter; //대화 패널의 일러스트
    public GameObject illustPanel; //일러스트 패널
    public GameObject illust; //일러스트

    [HideInInspector]
    public bool isDialogue = false; //플레이어가 대화중임을 나타내는 변수
    [HideInInspector]
    public DialogueManager _dialogueManager;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public bool kannaAnim = false; //Mansion 1F에서 칸나 애니메이션을 위한 변수
    public int maxDialogueCounter = 1; //플레이어가 진행할 대화의 수
    public int currentDialogueCounter = 1; //플레이어의 대화 진행상태
    [HideInInspector]
    public KannaController _kannaController;
    public KimsinController _kimsinController;
    public KentaController _kentaController;

    [Header("Interact")]
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 설정
    public Vector2 interactionAreaSize = new Vector2(2f, 1f); // 상호작용 영역의 크기
    public AudioSource _audioSource;

    [Header("Movement")]
    public float walkSpeed = 5f;

    public IInteractable interactable; //상호작용이 가능한 스크립트를 적용하기 위한 변수
    public bool interactRange = false;

    public IPlayerState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IPlayerState _idleState, _walkState, _waitState, _monoState, _diaState;


    private void Start()
    {
        monologuePanel.SetActive(false);
        dialoguePanel.SetActive(false);
        DirectionUtils.Initialize(this); // 플레이어 Direction 체크하는 함수 초기화

        _dialogueManager = FindObjectOfType<DialogueManager>();

        _kannaController = GameObject.FindGameObjectWithTag("Kanna").GetComponent<KannaController>();

        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _monoState = gameObject.AddComponent<PlayerMonologueState>();
        _diaState = gameObject.AddComponent<PlayerDialogueState>();

        _rigidbody = GetComponent<Rigidbody2D>();

        ChangeState(_waitState);
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
        // X축으로 이동중일 때에 이동하는 방향에 따라 왼쪽, 오른쪽 영역 적용
        if (CurrentDirection.x != 0)
        {
            Vector2 location = new(anim.GetFloat("DirX"), 0.35f);
            interactionAreaSize = new(2.5f, 2.0f);
            centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        }
        //Y축으로 이동중일 때에 이동하는 방향에 따라 위, 아래 영역 적용
        else if (CurrentDirection.y != 0)
        {
            Vector2 location = new(0.0f, anim.GetFloat("DirY"));
            interactionAreaSize = new(5.5f, 1.5f);
            centerPosition = (Vector2)transform.position + CurrentDirection * (interactionAreaSize);
        }


        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(centerPosition, interactionAreaSize, 0f, interactableLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                StartInteract();
                Debug.Log("상호작용 대상: " + hitCollider.gameObject.name);
                // 가장 가까운 하나의 오브젝트와만 상호작용하려면 여기서 break;
                break;
            }
        }
    }

    public void StartInteract()
    {
        interactable.Interact();
    }


    // 디버그를 위한 기즈모 그리기 (에디터에서만 표시됨)
    private void OnDrawGizmosSelected()
    {
        Vector2 location = new(0.0f, 1f);
        Gizmos.color = Color.yellow;
        Vector2 centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
    }

    public void MoveDownPlayer()
    {
        StartCoroutine(MovePlayer());
    }
    IEnumerator MovePlayer() //플레이어를 이동시키는 함수
    {
        Vector3 moveDown = new(transform.position.x, transform.position.y - 8, transform.position.z);
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDown, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        anim.SetBool("Walk", false);
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", 1.0f);
    }
}
