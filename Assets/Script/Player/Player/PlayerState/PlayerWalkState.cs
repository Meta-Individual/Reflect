using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalkState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Vector2 movement;

    public void OnStateEnter(PlayerController playerController)
    {
        if(!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Walk", true);
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            //Debug.Log("Player Walk");
            if (movement.x == 0)
            {
                movement.y = Input.GetAxisRaw("Vertical");
            }
            if (movement.y == 0)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
            }

            if (movement.x != 0 || movement.y != 0)
            {
                _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + movement * _playerController.walkSpeed * Time.fixedDeltaTime);
                _playerController.anim.SetFloat("DirX", movement.x);
                _playerController.anim.SetFloat("DirY", movement.y);

                // _playerController._rigidbody.velocity = new Vector2(movement.x*_playerController.walkSpeed, movement.y*_playerController.walkSpeed);
                // if (_playerController._rigidbody.velocity.magnitude > 0)
                // {
                //     if (!_playerController._audioSrc.isPlaying)
                //         _playerController._audioSrc.Play();
                // }
                // else
                // {
                //     _playerController._audioSrc.Stop();
                // }
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }
        }
    }
    public void OnStateExit()
    {
        _playerController.anim.SetBool("Walk", false);
    }

}
