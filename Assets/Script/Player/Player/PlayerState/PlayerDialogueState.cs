using System;
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
        if (Input.GetKeyDown(KeyCode.E) && !_playerController.isDialogue) //대화중이 아닐 때 E키를 누르면 다음 대사로 넘어감
        {   
            if(_playerController.currentDialogueCounter <= _playerController.maxDialogueCounter) //정해진 대화까지 Counter 증가하면서 대사 실행
            {
                _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
            }
            else
            {
                _playerController.dialoguePanel.SetActive(false); //정해진 대사만큼 대화가 끝났다면 대화창 비활성화
                _playerController.ChangeState(_playerController._idleState);
            }
        }
    }

    public void OnStateExit()
    {
        _playerController.dialoguePanel.SetActive(false);
        _playerController.anim.SetBool("Dialogue", false);
    }
}
