﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaAnimation : MonoBehaviour, IInteractable
{
    public string objectID;
    public string itemName = "KannaInteract";
    public Sprite image;

    private bool isSearched = false;
    private IllustManager _illustManager;
    private PlayerController _playerController;
    private PlayerInventory playerInventory;


    void Start()
    {
        _illustManager = FindObjectOfType<IllustManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            _illustManager.ShowIllust(image, objectID);
            _playerController.kannaAnim = true;
            playerInventory.AddItem(itemName);
        }
        else
        {
            _illustManager.ShowIllust(image, objectID);
        }
    }

}
