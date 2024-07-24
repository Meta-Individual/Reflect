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
    public string dialogueText; // ��ȣ�ۿ� �� ǥ���� �ؽ�Ʈ
    public GameObject dialoguePanel; // ��ǳ�� �г�
    public Transform player; // �÷��̾� Transform
    public Direction direction;

    private PlayerController playerController;
    private Animator anim;
    private bool isPlayerInRange; // �÷��̾ ������Ʈ ���� ���� �ִ��� ����
    private bool isStart = false;
    private Vector3 offset = new Vector3(0, 18f, 0); // ��ǳ���� ��ġ ������

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // ���� �� ��ǳ�� �г� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // �÷��̾ ������Ʈ ���� ���� �ְ� E Ű�� ������ ��ǳ�� ǥ��
        if (isPlayerInRange)
        {
            if (CheckDirection())
            {
                Debug.Log("���� ����");
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

        

        // ��ǳ�� �г��� ��ġ�� �÷��̾��� ���� ������Ʈ
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
