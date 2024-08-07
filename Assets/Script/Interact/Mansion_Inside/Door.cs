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
                 _monologueManager.ShowMonologue("���� ����־�. ���� �����ִ� �� ����");
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
        _monologueManager.ShowMonologue("���� ���Ⱦ�. � ������");
    }
}
