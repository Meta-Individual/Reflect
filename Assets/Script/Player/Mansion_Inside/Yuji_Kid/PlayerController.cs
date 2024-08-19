using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("SceneLoad")]
    public int mansionInside = 0;

    [Header("Animator")]
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


    [Header("Controller")]
    public KannaController _kannaController;
    public KimsinController _kimsinController;
    public KentaController _kentaController;

    [Header("Interact")]
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 설정
    public Vector2 interactionAreaSize = new Vector2(2f, 1f); // 상호작용 영역의 크기
    public IInteractable interactable; //상호작용이 가능한 스크립트를 적용하기 위한 변수
    public bool interactRange = false;

    [Header("Sound")]
    public AudioSource _audioSource;
    public AudioClip exclamationSound;
    public AudioClip getKeySound;
    public AudioClip paperSound;
    public AudioClip shakeSound;
    public AudioClip heartbeatSound;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    [Header("BoxCollider")]
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public GameObject gameObject4;
    public GameObject gameObject5;

    [Header("2F")]
    public Transform mansion2F_1;
    public Transform mansion2F_2;
    public int targetNum = 0;

    [Header("Camera")]
    public Camera _camera;
    public CameraShake _cameraShake;
    public CameraManager2 _cameraManager;
    public AudioSource cameraAudioSource;

    [Header("Monster")]
    public GameObject monster;
    public Transform monster_MovePoint;
    public Transform monster_MoveBackPoint;

    [Header("Flicker")]
    public Flicker _flicker;

    public static PlayerController Instance { get; private set; } // Singleton 인스턴스


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    public IPlayerState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IPlayerState _idleState, _walkState, _waitState, _monoState, _diaState, _runState;


    private void Start()
    {
        FadeManager.Instance.JustFade(); //화면 fade In

        monologuePanel.SetActive(false);
        dialoguePanel.SetActive(false);
        DirectionUtils.Initialize(this); // 플레이어 Direction 체크하는 함수 초기화

        _dialogueManager = FindObjectOfType<DialogueManager>();

        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _monoState = gameObject.AddComponent<PlayerMonologueState>();
        _diaState = gameObject.AddComponent<PlayerDialogueState>();
        _runState = gameObject.AddComponent<PlayerRunningState>();

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
        anim.SetBool("Wait", false);
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", -1.0f);

        while (transform.position != moveDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDown, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        anim.SetBool("Wait", true);
        anim.SetBool("Walk", false);
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", 1.0f);
    }

    public IEnumerator TransferTo2F() // 1초 정지후 Fade 시작동안 유우지, 카메라 위치 이동
    {
        ChangeState(_waitState);
        yield return new WaitForSeconds(1.0f);
        FadeManager.Instance.StartFade();
        yield return new WaitForSeconds(3.0f);
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", -1.0f);
        RecoverTransparency();
        if(targetNum == 1)
        {
            transform.position = mansion2F_1.position;
        }
        else if(targetNum == 2)
        {
            transform.position = mansion2F_2.position;
        }
        _camera.transform.position = new Vector3(mansion2F_1.position.x, mansion2F_1.position.y, _camera.transform.position.z);
        yield return new WaitForSeconds(2.0f);
        ChangeState(_idleState);
    }

    public void StartCameraShake() //카메라가 흔들리는 동안 플레이어가 대기 상태로 유지
    {
        StartCoroutine(WaitForDuration(_cameraShake.mOriginShakeDuration));
    }

    IEnumerator WaitForDuration(float duration) //1초 후 카메라 흔들림 연출
    {
        ChangeState(_waitState);
        maxDialogueCounter = 93;
        cameraAudioSource.clip = shakeSound;
        cameraAudioSource.Play();
        yield return new WaitForSeconds(1.0f);
        _cameraShake.ShakeCamera();
        _flicker.StartFlicker();
        yield return new WaitForSeconds(duration);
        cameraAudioSource.Stop();
        _dialogueManager.ShowDialogue(currentDialogueCounter.ToString());
    }

    public  void OnExclamation()
    {
        anim.SetBool("Exclamation", true);
    }


    public void OffExclamation() //플레이어 느낌표 이모티콘 OFF
    {
        anim.SetBool("Exclamation", false);
    }

    public void PlayExclamationSound() //느낌표 소리 재생
    {
        _audioSource.clip = exclamationSound;
        _audioSource.Play();
    }

    public void GetKeySound()
    {
        _audioSource.clip = getKeySound;
        _audioSource.Play();
    }

    public void PaperSound()
    {
        _audioSource.clip = paperSound;
        _audioSource.Play();
    }

    public void RecoverTransparency()
    {
        Color color = GetComponent<SpriteRenderer>().color;

        color.a = 1f;

        GetComponent<SpriteRenderer>().color = color;
    }



    /* ------------------------------------------------------ 저택 외부 전용------------------------------------------------------ */

    public void SetTurnBack() //유우지가 뒤돌고 1초후에 FadeIn 구현
    {
        StartCoroutine(StartTurnBack());
    }

    IEnumerator StartTurnBack()
    {
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", 1.0f);
        yield return new WaitForSeconds(1.0f);
        FadeManager.Instance.StartFadeIn();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MansionScene");
    }

    /* ------------------------------------------------------ 저택 내부 2회 전용------------------------------------------------------ */

    public void StartMonsterMoveCoroutine()
    {
        StartCoroutine(MonsterMove());
    }

    IEnumerator MonsterMove() //몬스터를 정해진 거실 위치까지 이동 후 대사 출력
    {
        while (monster.transform.position != monster_MovePoint.position)
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster_MovePoint.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(1.0f);

        maxDialogueCounter = 100;
        _dialogueManager.ShowDialogue(currentDialogueCounter.ToString());
    }

    public void StartMonsterMoveBackCoroutine()
    {
        StartCoroutine(MonsterMoveBack());
    }

    IEnumerator MonsterMoveBack() //몬스터를 정해진 거실 위치까지 이동 후 대사 출력
    {
        yield return new WaitForSeconds(2.0f);

        while (monster.transform.position != monster_MoveBackPoint.position)
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster_MoveBackPoint.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(1.0f);

        maxDialogueCounter = 101;
        _dialogueManager.ShowDialogue(currentDialogueCounter.ToString());
    }

}
