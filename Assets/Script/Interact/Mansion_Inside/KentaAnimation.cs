using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentaAnimation : MonoBehaviour, IInteractable
{
    public string itemName = "KentaInteract";
    public Direction currentDirection = Direction.UP;
    public int currentDialogueID = 47;
    public Animator anim;

    private bool isSearched = false;
    private PlayerController _playerController;
    private PlayerInventory playerInventory;


    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    public void Interact()
    {
        Debug.Log("Kenta 애니메이션 상호작용");
        if (!isSearched)
        {
            if (DirectionUtils.CheckDirection(currentDirection))
            {
                isSearched = true;
                playerInventory.AddItem(itemName);
                StartCoroutine(ActiveKentaAnimation());
            }
        }
    }

    IEnumerator ActiveKentaAnimation()
    {
        _playerController.ChangeState(_playerController._waitState);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("Out", true);
        yield return new WaitForSeconds(2.0f);
        _playerController.ChangeState(_playerController._diaState);
        _playerController.maxDialogueCounter = currentDialogueID;
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }
}
