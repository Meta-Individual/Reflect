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
    private bool playerInRange = false; // 플레이어가 포탈 위에 있는지 여부
    private AudioSource audioSource; // AudioSource 컴포넌트

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public Transform targetLocation; // 이동할 목표 위치
    public bool isLock = false;

    [Header("Sound")]
    public AudioClip openDoorSound; // 방문 여는 사운드
    public AudioClip closeDoorSound; // 방문 닫는 사운드

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                    if (isLock)
                    {
                        Debug.Log("잠김");
                    }
                    else
                    {
                        player.transform.position = targetLocation.position;
                        PlaySound(); // 방문 사운드 재생
                    }
                }
            }
            else
            {
                HideUI();
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
                Debug.Log("음원 파일이 존재하지 않거나, 잘못된 태그로 설정되었습니다.");
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
