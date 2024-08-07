using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    private MonologueManager _monologueManager;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
    }
    public void Interact()
    {
        string monologue = LoadMonologue.Instance.GetMonologue(objectID);
        _monologueManager.ShowMonologue(monologue);
    }

}
