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
    private bool playerInRange = false; // �÷��̾ ��Ż ���� �ִ��� ����
    private AudioSource audioSource; // AudioSource ������Ʈ

    public Text text;

    [Header("Target")]
    public Direction direction;
    public Transform targetLocation; // �̵��� ��ǥ ��ġ

    [Header("Sound")]
    public AudioClip openDoorSound; // �湮 ���� ����
    public AudioClip closeDoorSound; // �湮 �ݴ� ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        text = GameObject.Find("Tag").GetComponent<Text>();
        HideText();
    }

    void Update()
    {
        if (playerInRange)
        {
            if (CheckDirection())
            {
                ShowText();
                if (Input.GetKeyDown((KeyCode)CustomKey.Interact))
                {
                    player.transform.position = targetLocation.position;
                    PlaySound(); // �湮 ���� ���
                }
            }
            else
            {
                HideText();
            }
        }
    }

    private void PlaySound()
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


    private void ShowText()
    {
        Color color = text.color;
        color.a = 1f;
        text.color = color;

        text.transform.position = transform.position + new Vector3(0, 1, 0);
        if (CompareTag("OpenDoor"))
        {
            text.text = "����";
        }
        else if (CompareTag("CloseDoor"))
        {
            text.text = "�ݱ�";
        }
    }

    private void HideText()
    {
        Color color = text.color;
        color.a = 0f;
        text.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowText();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
            playerInRange = false;
        }
    }
}
