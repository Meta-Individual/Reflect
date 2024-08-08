using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public string objectID;
    private DialogueManager _dialogueManager;

    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }
    public void Interact()
    {
        (string characterName, string sprite, string dialogue) = LoadDialogue.Instance.GetMonologue(objectID);
        _dialogueManager.ShowDialogue(characterName, sprite, dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
