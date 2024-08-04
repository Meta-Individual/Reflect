using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isLocked = true;
    public string keyItemName = "DoorKey";
    public string nextSceneName;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
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
                ShowDialogue("���� ����־�.");
            }
        }
        else
        {
            // ���� ���� ������ �ٸ� ��ҷ� �̵�
            LoadNextScene();
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        ShowDialogue("���� ���Ⱦ�.");
    }

    private void ShowDialogue(string message)
    {
        Debug.Log(message); // ���� ���ӿ����� UI�� ǥ��
    }

    private void LoadNextScene()
    {
        // ���� ������� �̵��ϴ� ����
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
