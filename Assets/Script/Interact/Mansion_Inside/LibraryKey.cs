using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKey : MonoBehaviour, IInteractable
{
    public  string keyItemName = "LibraryDoorKey";
    private bool   isSearched = false;
    public string getKeyMonologue = "���� ���踦 �����. Ư���� ���� ���� �� ����.";
    public  string objectID;
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            playerInventory.AddItem(keyItemName);
            _monologueManager.ShowMonologue(getKeyMonologue);
        }
        else
        {
            string dialogue = DialogueManager.Instance.GetDialogue(objectID);
            _monologueManager.ShowMonologue(dialogue);
        }
    }
}
