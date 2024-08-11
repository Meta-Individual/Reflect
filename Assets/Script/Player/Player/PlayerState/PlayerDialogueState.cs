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
            else if(_playerController.currentDialogueCounter == 21) //칸나가 걸어서 거실로 나가는 부분
            {
                _playerController._npcController.goToLivingRoom = true;
                _playerController.ChangeState(_playerController._waitState);
            }
            else if(_playerController.currentDialogueCounter == 48) //켄타를 찾아서 옷장에서 나오는 부분
            {
                _playerController.ChangeState(_playerController._waitState);
                //대사가 다 끝난 경우, wait 상태에서 2초 정도 대기한 후, Fade in Fade out 연출로 거실에서 시작.
                //fade out 후에도 2초 정도 대기 시간이 있도록 ! 이후에는 바로 다음 대사 진행
            }
            else
            {
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
