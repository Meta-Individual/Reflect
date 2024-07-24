using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    public enum Direction
    {
        RIGHT, LEFT, UP, DOWN
    }

    [Header("Monologue")]
    public string dialogueText; // 상호작용 시 표시할 텍스트
    public GameObject dialoguePanel; // 말풍선 패널
    public Transform player; // 플레이어 Transform
    public Direction direction;

    private PlayerController playerController;
    private Animator anim;
    private bool isPlayerInRange; // 플레이어가 오브젝트 범위 내에 있는지 여부
    private bool isStart = false;
    private Vector3 offset = new Vector3(0, 18f, 0); // 말풍선의 위치 오프셋

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // 시작 시 말풍선 패널 비활성화
        }
    }

    void Update()
    {
        // 플레이어가 오브젝트 범위 내에 있고 E 키를 누르면 말풍선 표시
        if (isPlayerInRange)
        {
            if (CheckDirection())
            {
                Debug.Log("독백 가능");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (dialoguePanel != null && !isStart)
                    {
                        dialoguePanel.SetActive(true);
                        dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogueText;
                        dialoguePanel.transform.position = player.position + offset;
                        isStart = true;
                        playerController.ChangeState(playerController._waitState);
                    }
                    else
                    {
                        dialoguePanel.SetActive(false);
                        dialoguePanel.GetComponentInChildren<TMP_Text>().text = "";
                        isStart = false;
                        playerController.ChangeState(playerController._idleState);
                    }
                }
            }
        }

        

        // 말풍선 패널의 위치를 플레이어의 위로 업데이트
        if (dialoguePanel.activeSelf)
        {
            dialoguePanel.transform.position = player.position + offset;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
