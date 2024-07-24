using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    public enum Direction
    {
        RIGHT, LEFT, UP, DOWN
    }

    [Header("Monologue")]
    public List<string> dialogueText; // 상호작용 시 표시할 텍스트
    public List<float> delay; // 각 글자 사이의 지연 시간
    public GameObject dialoguePanel; // 말풍선 패널
    public Transform player; // 플레이어 Transform
    public Direction direction;

    private PlayerController playerController;
    private Animator anim;
    private bool isPlayerInRange; // 플레이어가 오브젝트 범위 내에 있는지 여부
    private bool isFinish = false;
    private bool isTalking = false;
    private int currentLineIndex = 0; // 현재 대사 인덱스
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
                if(!isTalking)
                {
                    Debug.Log("독백 가능");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (isFinish) 
                        {
                            dialoguePanel.SetActive(false);
                            dialoguePanel.GetComponentInChildren<TMP_Text>().text = "";
                            playerController.ChangeState(playerController._idleState);
                            currentLineIndex = 0;
                            isFinish = false;
                        }
                        else
                        {
                            if (dialoguePanel != null)
                            {
                                dialoguePanel.SetActive(true);
                                StartCoroutine(ActiveMonologue());
                                dialoguePanel.transform.position = player.position + offset;
                                playerController.ChangeState(playerController._waitState);
                            }
                        }
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

    private IEnumerator ActiveMonologue()
    {
        isTalking = true;
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = ""; // 텍스트 초기화
        
        foreach (char letter in dialogueText[currentLineIndex].ToCharArray())
        {
            dialoguePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(delay[currentLineIndex]); // 지연 시간 대기
        }

        // 다음 대사로 이동
        isTalking = false;
        if (currentLineIndex < dialogueText.Count - 1)
        {
            currentLineIndex++;
        }
        else
        {
            isFinish = true;
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
