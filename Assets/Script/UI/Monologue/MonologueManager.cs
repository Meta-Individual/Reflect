using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    public string dialogueText; // ��ȣ�ۿ� �� ǥ���� �ؽ�Ʈ
    public GameObject dialoguePanel; // ��ǳ�� �г�

    private bool isPlayerInRange; // �÷��̾ ������Ʈ ���� ���� �ִ��� ����

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // ���� �� ��ǳ�� �г� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // �÷��̾ ������Ʈ ���� ���� �ְ� E Ű�� ������ ��ǳ�� ǥ��
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
                dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogueText;
            }
        }
        // �÷��̾ ������Ʈ ���� ������ ����� ��ǳ�� ��Ȱ��ȭ
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
