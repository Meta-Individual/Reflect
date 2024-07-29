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
    public string doorOpendText;
    public string doorClosedText;
    public float delay; // �� ���� ������ ���� �ð�
    public GameObject dialoguePanel; // ��ǳ�� �г�
    public Transform player; // �÷��̾� Transform
    public Direction direction;

    private TransferMap _transferMap;
    private PlayerController playerController;
    private Animator anim;
    private bool isPlayerInRange; // �÷��̾ ������Ʈ ���� ���� �ִ��� ����
    private bool isFinish = false;
    private bool isTalking = false;
    private Vector3 offset = new(0, 15f, 0); // ��ǳ���� ��ġ ������

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _transferMap = GetComponent<TransferMap>();
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
                if (!isTalking)
                {
                    Debug.Log("���� ����");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (isFinish)
                        {
                            dialoguePanel.SetActive(false);
                            dialoguePanel.GetComponentInChildren<TMP_Text>().text = "";
                            playerController.ChangeState(playerController._idleState);
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

        // ��ǳ�� �г��� ��ġ�� �÷��̾��� ���� ������Ʈ
        if (dialoguePanel.activeSelf)
        {
            dialoguePanel.transform.position = player.position + offset;
        }
    }

    private IEnumerator ActiveMonologue()
    {
        isTalking = true;
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = ""; // �ؽ�Ʈ �ʱ�ȭ


        if (_transferMap.isLock)
        {
            if (!playerController.libraryKey)
            {
                foreach (char letter in doorClosedText)
                {
                    dialoguePanel.GetComponentInChildren<TMP_Text>().text += letter;
                    yield return new WaitForSeconds(delay); // ���� �ð� ���
                }
            }
            else
            {
                foreach (char letter in doorOpendText)
                {
                    dialoguePanel.GetComponentInChildren<TMP_Text>().text += letter;
                    yield return new WaitForSeconds(delay); // ���� �ð� ���
                }
                _transferMap.isLock = false;
            }
            isFinish = true;
        }

        // ���� ���� �̵�
        isTalking = false;
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
