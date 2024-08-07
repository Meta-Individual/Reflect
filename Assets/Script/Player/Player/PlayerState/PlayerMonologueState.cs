using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologueState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private NPCController _npcController;

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
            if (_playerController.kannaAnim)
            {
                _playerController.kannaAnim = false;
                _playerController._npcController.ChangeState(_playerController._npcController._outState);
            }
            _playerController.ChangeState(_playerController._idleState);
        }
    }

    public void OnStateExit()
    {
        _playerController.monologuePanel.SetActive(false);
        _playerController.anim.SetBool("Monologue", false);
    }
}
