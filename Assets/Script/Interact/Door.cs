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
                ShowDialogue("문이 잠겨있어.");
            }
        }
        else
        {
            // 문이 열려 있으면 다른 장소로 이동
            LoadNextScene();
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        ShowDialogue("문이 열렸어.");
    }

    private void ShowDialogue(string message)
    {
        Debug.Log(message); // 실제 게임에서는 UI로 표시
    }

    private void LoadNextScene()
    {
        // 다음 장면으로 이동하는 로직
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
