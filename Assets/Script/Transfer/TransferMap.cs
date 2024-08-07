using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferMap : MonoBehaviour
{
    public enum Direction
    {
        RIGHT,
        LEFT,
        UP,
        DOWN
    }
    private Animator anim;
    private GameObject player;
    private AudioSource audioSource; // AudioSource ������Ʈ
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;
    private Camera _camera;
    private bool playerInRange = false; // �÷��̾ ��Ż ���� �ִ��� ����
    private bool isMonologue = false;

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public Transform targetLocation; // �̵��� ��ǥ ��ġ
    public bool isLocked = false;
    public string keyItemName = "Key";
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
    }

    void Update()
    {
        if (playerInRange)
        {
            if (CheckDirection())
            {
                ShowUI();

                if (Input.GetKeyDown((KeyCode)CustomKey.Interact))
                {
                    if(!isMonologue)
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
                            TransformWithSound(); // �湮 ���� ���
                        }
                    }
                    else
                    {
                        isMonologue = false;

                    }
                }
            }
            else
            {
                HideUI();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
