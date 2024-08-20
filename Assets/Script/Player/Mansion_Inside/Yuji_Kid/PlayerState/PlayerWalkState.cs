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

            // 첫 입력 방향 설정 (선입력 방향이 여전히 활성화되어 있지 않을 때만 업데이트)
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

            // 입력이 없는 경우 방향 초기화
            if (movement == Vector2.zero)
            {
                firstInputDirection = Vector2.zero;
            }

            // 선입력된 방향의 애니메이션 재생
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

            // 선입력된 방향의 키가 떼어지면 새 방향 설정
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
        movement.x = 0;
        movement.y = 0;
        _playerController.anim.SetBool("Walk", false);
    }

}
