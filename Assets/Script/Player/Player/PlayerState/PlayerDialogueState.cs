using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _playerController.anim.SetBool("Dialogue", true);
    }
    public void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }

    public void OnStateExit()
    {
        _playerController.dialoguePanel.SetActive(false);
        _playerController.anim.SetBool("Dialogue", false);
    }
}
