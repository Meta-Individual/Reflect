using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologueState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("Monologue", true);
    }
    public void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerController.monologuePanel.SetActive(false);
            _playerController.ChangeState(_playerController._idleState);
        }
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Monologue", false);
    }
}
