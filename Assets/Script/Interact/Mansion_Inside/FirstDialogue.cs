using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialogue : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    public PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (_playerController.mansionInside == 0)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(2.0f);
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
        _playerController.maxDialogueCounter = currentDialogueID;
    }
    
}
