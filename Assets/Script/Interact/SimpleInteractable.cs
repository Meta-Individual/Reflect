using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;

    public void Interact()
    {
        string dialogue = DialogueManager.Instance.GetDialogue(objectID);
        ShowDialogue(dialogue);
    }

    private void ShowDialogue(string message)
    {
        Debug.Log(message); // 실제 게임에서는 UI로 표시
    }
}
