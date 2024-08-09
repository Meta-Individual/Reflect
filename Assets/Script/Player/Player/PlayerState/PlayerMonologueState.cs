using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologueState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _playerController.anim.SetBool("Monologue", true);
    }
    public void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerController.ChangeState(_playerController._idleState);
            if (_playerController.kannaAnim)
            {
                _playerController.kannaAnim = false;
                _playerController._npcController.ChangeState(_playerController._npcController._outState);
            }
        }
    }

    public void OnStateExit()
    {
        _playerController.monologuePanel.SetActive(false);
        _playerController.anim.SetBool("Monologue", false);
    }
}
