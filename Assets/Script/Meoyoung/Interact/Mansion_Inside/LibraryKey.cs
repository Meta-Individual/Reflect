using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKey : MonoBehaviour, IInteractable
{
    public  string keyItemName = "LibraryDoorKey";
    private bool   isSearched = false;
    public string afterObjectID;
    public string objectID;
    public Sprite image;
    private IllustManager _illustManager;
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;

    [SerializeField] GameObject particle;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _illustManager = FindObjectOfType<IllustManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!particle.activeSelf)
        {
            particle.SetActive(true);
        }
    }

    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            playerInventory.AddItem(keyItemName);
            _playerController.GetKeySound();
            _illustManager.ShowIllust(image, afterObjectID);
            particle.SetActive(false);
        }
        else
        {
            _monologueManager.ShowMonologue(objectID);
        }
    }
}
