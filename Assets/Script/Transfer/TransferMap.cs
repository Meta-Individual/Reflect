using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferMap : MonoBehaviour, IInteractable
{
    public enum Direction //플레이어가 바라보고 있는 방향을 얻기위한 변수
    {
        RIGHT,
        LEFT,
        UP,
        DOWN
    }
    private Animator         anim;
    private GameObject       player;
    private AudioSource      audioSource; // AudioSource 컴포넌트
    private PlayerInventory  playerInventory;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;
    private Camera           _camera;
    private bool             playerInRange = false; // 플레이어가 포탈 위에 있는지 여부
    private bool             isMonologue = false;

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public Transform targetLocation; // 이동할 목표 위치
    public bool isLocked = false;
    public string keyItemName = "Key";
    public bool stair = false;
    [Header("Sound")]
    public AudioClip openDoorSound; // 방문 여는 사운드
    public AudioClip closeDoorSound; // 방문 닫는 사운드
    [Header("Script")]
    public string doorClosedScript = "문이 잠겨있어.";
    public string doorOpendScript = "문이 열렸어";

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _playerController = player.GetComponent<PlayerController>();
        HideUI();
    }

    void Update()
    {
        if (playerInRange)
        {
            if (CheckDirection())
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown((KeyCode)CustomKey.Interact) && (_playerController.CurrentState == _playerController._idleState || _playerController.CurrentState == _playerController._walkState))
        {
            if (!isMonologue)
            {
                if (isLocked)
                {
                    if (playerInventory.HasItem(keyItemName))
                    {
                        UnlockDoor();
                    }
                    else
                    {
                        _monologueManager.ShowMonologue(doorClosedScript);
                        isMonologue = true;
                    }
                }
                else
                {
                    TransformWithSound(); // 방문 사운드 재생
                }
            }
            else
            {
                isMonologue = false;

            }
        }
    }
    public void UnlockDoor()
    {
        isLocked = false;
        isMonologue = true;
        _monologueManager.ShowMonologue(doorOpendScript);
    }

    public void TransformWithSound()
    {
        if (audioSource != null)
        {
            if (CompareTag("OpenDoor") && openDoorSound != null)
            {
                audioSource.clip = openDoorSound;
                audioSource.Play();
            }
            else if (CompareTag("CloseDoor") && closeDoorSound != null)
            {
                audioSource.clip = closeDoorSound;
                audioSource.Play();
            }
            else
            {
                Debug.Log("음원 파일이 존재하지 않거나, 잘못된 태그로 설정되었습니다.");
            }
        }
        if (stair)
        {
            if (direction == Direction.RIGHT)
            {
                anim.SetFloat("DirX", -1.0f);
            }
            else if (direction == Direction.LEFT)
            {
                anim.SetFloat("DirX", 1.0f);

            }
            else if (direction == Direction.UP)
            {
                anim.SetFloat("DirY", -1.0f);

            }
            else if (direction == Direction.DOWN)
            {
                anim.SetFloat("DirY", 1.0f);

            }
        }
        player.transform.position = targetLocation.position;
        _camera.transform.position = new (targetLocation.position.x, targetLocation.position.y, _camera.transform.position.z);
    }

    private bool CheckDirection()
    {
        if (direction == Direction.RIGHT)
        {
            if (anim.GetFloat("DirX") == 1)
            {
                return true;
            }
        }
        else if (direction == Direction.LEFT)
        {
            if (anim.GetFloat("DirX") == -1)
            {
                return true;
            }
        }
        else if (direction == Direction.UP)
        {
            if (anim.GetFloat("DirY") == 1)
            {
                return true;
            }
        }
        else if (direction == Direction.DOWN)
        {
            if (anim.GetFloat("DirY") == -1)
            {
                return true;
            }
        }
        return false;
    }


    private void ShowUI()
    {
        arrow_UI.SetActive(true);
    }

    private void HideUI()
    {
        arrow_UI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (CheckDirection())
            {
                GetInteractScript();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            RemoveInteractScript();
        }
    }

    private void GetInteractScript()
    {
        _playerController.interactable = this.GetComponent<IInteractable>();
        _playerController.interactRange = true;
    }

    private void RemoveInteractScript()
    {
        _playerController.interactRange = false;
        if (_playerController.interactable != null)
        {
            _playerController.interactable = null;
        }
    }
}
