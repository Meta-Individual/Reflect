using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalkState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Vector2 movement;
    private Vector2 firstInputDirection;
    private bool firstInputSet;


    public void OnStateEnter(PlayerController playerController)
    {
        if(!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Walk", true);
        movement.x = 0;
        movement.y = 0;
        firstInputDirection = Vector2.zero;
        firstInputSet = false;
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // ù �Է� ���� ���� (���Է� ������ ������ Ȱ��ȭ�Ǿ� ���� ���� ���� ������Ʈ)
            if (firstInputDirection == Vector2.zero)
            {
                if (movement.x != 0)
                {
                    firstInputDirection = new Vector2(movement.x, 0);
                }
                else if (movement.y != 0)
                {
                    firstInputDirection = new Vector2(0, movement.y);
                }
            }

            // �Է��� ���� ��� ���� �ʱ�ȭ
            if (movement == Vector2.zero)
            {
                firstInputDirection = Vector2.zero;
            }

            // ���Էµ� ������ �ִϸ��̼� ���
            if (firstInputDirection.x > 0)
            {
                _playerController.anim.SetFloat("DirX", 1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (firstInputDirection.x < 0)
            {
                _playerController.anim.SetFloat("DirX", -1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (firstInputDirection.y > 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", 1.0f);
            }
            else if (firstInputDirection.y < 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", -1.0f);
            }

            // ���Էµ� ������ Ű�� �������� �� ���� ����
            if (firstInputDirection.x != 0 && movement.x == 0)
            {
                firstInputDirection = Vector2.zero;
            }
            else if (firstInputDirection.y != 0 && movement.y == 0)
            {
                firstInputDirection = Vector2.zero;
            }
           

            if (movement.x != 0 || movement.y != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _playerController.ChangeState(_playerController._runState);
                }
                _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + movement.normalized * _playerController.walkSpeed * Time.fixedDeltaTime);
                
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }

            if (Input.GetKeyDown(KeyCode.E)) // 'E' Ű�� ��ȣ�ۿ�
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
        movement.x = 0;
        movement.y = 0;
        _playerController.anim.SetBool("Walk", false);
    }

}
