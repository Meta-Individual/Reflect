using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaAnimation : MonoBehaviour, IInteractable
{
    public string objectID;
    private bool isSearched = false;
    public Sprite image;
    private IllustManager _illustManager;
    private PlayerController _playerController;

    void Start()
    {
        _illustManager = FindObjectOfType<IllustManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            string monologue = LoadMonologue.Instance.GetMonologue(objectID);
            _illustManager.ShowIllust(image, monologue);
            _playerController.kannaAnim = true;
        }
        else
        {
            string monologue = LoadMonologue.Instance.GetMonologue(objectID);
            _illustManager.ShowIllust(image, monologue);
        }
    }

}
