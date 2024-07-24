using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    public string dialogueText; // 상호작용 시 표시할 텍스트
    public GameObject dialoguePanel; // 말풍선 패널

    private bool isPlayerInRange; // 플레이어가 오브젝트 범위 내에 있는지 여부

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // 시작 시 말풍선 패널 비활성화
        }
    }

    void Update()
    {
        // 플레이어가 오브젝트 범위 내에 있고 E 키를 누르면 말풍선 표시
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
                dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogueText;
            }
        }
        // 플레이어가 오브젝트 범위 내에서 벗어나면 말풍선 비활성화
        else if (!isPlayerInRange && dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
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
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
        }
    }
}
