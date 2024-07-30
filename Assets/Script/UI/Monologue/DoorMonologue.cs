using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorMonologue : MonoBehaviour
{
    public enum Direction
    {
        RIGHT, LEFT, UP, DOWN
    }

    [Header("Monologue")]
    public string doorOpenText;
    public string doorClosedText;
    public float delay; // 각 글자 사이의 지연 시간
    public GameObject dialoguePanel; // 말풍선 패널
    public Transform player; // 플레이어 Transform
    public Direction direction;

    private TransferMap transferMap;
    private PlayerController playerController;
    private Animator animator;
    private bool isPlayerInRange; // 플레이어가 오브젝트 범위 내에 있는지 여부
    private bool isMonologueFinished = false;
    private bool isTalking = false;
    private Vector3 offset = new(0, 15f, 0); // 말풍선의 위치 오프셋

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        transferMap = GetComponent<TransferMap>();
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // 시작 시 말풍선 패널 비활성화
        }
    }

    void Update()
    {
        if (!transferMap.isLock)
        {
            return; // transferMap이 잠겨있지 않으면 더 이상 업데이트하지 않음
        }

        if (isPlayerInRange && CheckDirection() && !isTalking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isMonologueFinished)
                {
                    EndMonologue();
                }
                else
                {
                    StartMonologue();
                }
            }
        }

        if (dialoguePanel.activeSelf)
        {
            UpdateDialoguePanelPosition();
        }
    }

    private void StartMonologue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(DisplayMonologue());
            playerController.ChangeState(playerController._waitState);
        }
    }

    private void EndMonologue()
    {
        dialoguePanel.SetActive(false);
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = "";
        playerController.ChangeState(playerController._idleState);
        isMonologueFinished = false;

        if (transferMap.isLock && playerController.libraryKey)
        {
            transferMap.isLock = false;
        }
    }

    private void UpdateDialoguePanelPosition()
    {
        dialoguePanel.transform.position = player.position + offset;
    }

    private IEnumerator DisplayMonologue()
    {
        isTalking = true;
        var textComponent = dialoguePanel.GetComponentInChildren<TMP_Text>();
        textComponent.text = ""; // 텍스트 초기화

        string textToDisplay = transferMap.isLock && !playerController.libraryKey ? doorClosedText : doorOpenText;

        foreach (char letter in textToDisplay)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay); // 지연 시간 대기
        }

        /*if (transferMap.isLock && playerController.libraryKey)
        {
            transferMap.isLock = false;
        }*/

        isMonologueFinished = true;
        isTalking = false;
    }

    private bool CheckDirection()
    {
        float dirX = animator.GetFloat("DirX");
        float dirY = animator.GetFloat("DirY");

        return (direction == Direction.RIGHT && dirX == 1) ||
               (direction == Direction.LEFT && dirX == -1) ||
               (direction == Direction.UP && dirY == 1) ||
               (direction == Direction.DOWN && dirY == -1);
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
