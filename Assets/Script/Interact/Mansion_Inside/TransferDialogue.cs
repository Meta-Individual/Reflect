using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferDialog : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void Interact()
    {
        if (startDialogueID == _playerController.currentDialogueCounter)
        {
            (string characterName, string sprite, string dialogue) = LoadDialogue.Instance.GetDialogue(_playerController.currentDialogueCounter.ToString());
            //Debug.Log("ĳ�����̸� : " + characterName + " ��������Ʈ ���� : " + sprite + " ��� : " + dialogue);
            _playerController._dialogueManager.ShowDialogue(characterName, sprite, dialogue);
            _playerController.maxDialogueCounter = currentDialogueID;
            _playerController.ChangeState(_playerController._diaState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
