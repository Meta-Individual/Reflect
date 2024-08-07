using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isLocked = true;
    public string keyItemName = "LibraryDoorKey";

    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;
    private TransferMap _transferMap;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _transferMap = GetComponent<TransferMap>();
    }

    public void Interact()
    {
        if (isLocked)
        {
            if (playerInventory.HasItem(keyItemName))
            {
                UnlockDoor();
            }
            else
            {
                 _monologueManager.ShowMonologue("문이 잠겨있어. 여기 숨어있는 것 같아");
            }
        }
        else
        {
            _transferMap.TransformWithSound();
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        _monologueManager.ShowMonologue("문이 열렸어. 어서 들어가보자");
    }
}
