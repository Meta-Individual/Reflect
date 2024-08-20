﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Vector2 movement;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Run", true);
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            if (movement.x == 0)
            {
                movement.y = Input.GetAxisRaw("Vertical");
                _playerController.CurrentDirection = new(0.0f, movement.y);
            }
            if (movement.y == 0)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                _playerController.CurrentDirection = new(movement.x, 0.0f);
            }

            if (movement.x != 0 || movement.y != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + movement * _playerController.runSpeed * Time.fixedDeltaTime);
                    _playerController.anim.SetFloat("DirX", movement.x);
                    _playerController.anim.SetFloat("DirY", movement.y);
                }
                if(Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _playerController.ChangeState(_playerController._walkState);
                }
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }

            if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상호작용
            {
                if (_playerController.interactRange)
                {
                    _playerController.StartInteract();
                }
                else
                {
                    _playerController.Interact();
                }
            }
        }
    }
    public void OnStateExit()
    {
        _playerController.anim.SetBool("Run", false);
    }

}