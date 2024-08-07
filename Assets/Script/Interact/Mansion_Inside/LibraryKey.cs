﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKey : MonoBehaviour, IInteractable
{
    public  string keyItemName = "LibraryDoorKey";
    private bool   isSearched = false;
    public string getKeyMonologue = "서재 열쇠를 얻었다. 특별한 것은 없는 것 같다.";
    public  string objectID;
    public Sprite image;
    private IllustManager _illustManager;
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _illustManager = FindObjectOfType<IllustManager>();
    }

    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            playerInventory.AddItem(keyItemName);
            _illustManager.ShowIllust(image, getKeyMonologue);
        }
        else
        {
            string dialogue = LoadMonologue.Instance.GetMonologue(objectID);
            _monologueManager.ShowMonologue(dialogue);
        }
    }
}
