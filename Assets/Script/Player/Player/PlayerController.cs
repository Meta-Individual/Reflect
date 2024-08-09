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
    public TMP_Text   characterName; //대화 패널의 캐릭터 이름
    public TMP_Text   dialogue; //대화 패널의 대사
    public Image      charcter; //대화 패널의 일러스트
    public GameObject illustPanel; //일러스트 패널
    public GameObject illust; //일러스트

    [HideInInspector]
    public bool isDialogue = false; //플레이어가 대화중임을 나타내는 변수
    [HideInInspector]
    public DialogueManager _dialogueManager;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public bool kannaAnim = false; //Mansion 1F에서 칸나 애니메이션을 위한 변수
    public int maxDialogueCounter = 1; //플레이어가 진행할 대화의 수
    public int currentDialogueCounter = 1; //플레이어의 대화 진행상태
    [HideInInspector]
    public NPCController _npcController;

    [Header("Interact")]
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 설정
    public Vector2 interactionAreaSize = new Vector2(2f, 1f); // 상호작용 영역의 크기

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

    public IPlayerState _idleState, _walkState, _waitState, _monoState, _diaState;


    private void Start()
    {
        monologuePanel.SetActive(false);
        dialoguePanel.SetActive(false);

        _dialogueManager = FindObjectOfType<DialogueManager>();

        _npcController = GameObject.FindGameObjectWithTag("Kanna").GetComponent<NPCController>();

        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _monoState = gameObject.AddComponent<PlayerMonologueState>();
        _diaState = gameObject.AddComponent<PlayerDialogueState>();

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
                //Debug.Log("상호작용 대상: " + hitCollider.gameObject.name);
                break;
            }
        }
    }

    // 디버그를 위한 기즈모 그리기 (에디터에서만 표시됨)
    private void OnDrawGizmosSelected()
    {
        Vector2 location = new(0.0f, 1f);
        Gizmos.color = Color.yellow;
        Vector2 centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
    }

    public void MovePlayer(float x, float y, float z) //플레이어를 이동시키는 함수
    {
        transform.position += new Vector3(x, y, z);
    }
}
