using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour, IInteractable
{
    public enum Direction //�÷��̾ �ٶ󺸰� �ִ� ������ ������� ����
    {
        RIGHT,
        LEFT,
        UP,
        DOWN
    }
    private Animator anim;
    private GameObject player;
    public AudioSource audioSource; // AudioSource ������Ʈ
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;
    private Camera _camera;
    private bool playerInRange = false; // �÷��̾ ��Ż ���� �ִ��� ����
    private bool isMonologue = false;

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public bool isLocked = false;
    public string keyItemName = "Key";
    public bool stair = false;
    [Header("Sound")]
    public AudioClip openDoorSound; // �湮 ���� ����
    public AudioClip closeDoorSound; // �湮 �ݴ� ����
    [Header("Script")]
    public string doorClosedScript = "���� ����־�.";
    public string doorOpendScript = "���� ���Ⱦ�";

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

        playerInventory.AddItem("Outside");
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
                        TransformWithSound(); // �湮 ���� ���
                    }
                    else
                    {
                        _monologueManager.ShowMonologue(doorClosedScript);
                        isMonologue = true;
                    }
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
                Debug.Log("���� ������ �������� �ʰų�, �߸��� �±׷� �����Ǿ����ϴ�.");
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
        SceneManager.LoadScene("MansionOutScene");
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

