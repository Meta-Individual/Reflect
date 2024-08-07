using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    public Sprite image;
    private IllustManager _illustManager;

    void Start()
    {
        _illustManager = FindObjectOfType<IllustManager>();
    }
    public void Interact()
    {
        string monologue = LoadMonologue.Instance.GetMonologue(objectID);
        _illustManager.ShowIllust(image, monologue);
    }
}
