using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaAnim : MonoBehaviour, IInteractable
{
    private bool isSearched = false;
    public string kannaMonologue = "책상 밑에 뭔가 있는 것 같다..";
    public string objectID;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            _monologueManager.ShowMonologue(kannaMonologue);
            _playerController.kannaAnim = true;
        }
        else
        {
            string dialogue = DialogueManager.Instance.GetDialogue(objectID);
            _monologueManager.ShowMonologue(dialogue);
        }
    }
}
