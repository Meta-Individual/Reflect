﻿using System;
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
                if (_playerController.currentDialogueCounter == 3) // 숨바꼭질 시작할 때 눈 뜨는 연출
                {
                    FadeManager.Instance.StartFadeOut();
                }
                else if (_playerController.currentDialogueCounter == 78) //켄타가 현관으로 나가는 대사
                {
                    Debug.Log("켄타 나가자");
                    _playerController._kentaController.MoveKenta();
                }
                else if (_playerController.currentDialogueCounter == 82) //칸나가 켄타를 따라서 현관으로 나가는 대사
                {
                    Debug.Log("칸나 나가자");
                    _playerController._kannaController.MoveKanna();
                }
                else if(_playerController.currentDialogueCounter == 92)
                {
                    _playerController._kimsinController.RunMoveOutCoroutine();
                }
                _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
            }
            else if(_playerController.currentDialogueCounter == 21) //칸나가 걸어서 거실로 나가는 부분
            {
                _playerController._kannaController.goToLivingRoom = true;
                _playerController.ChangeState(_playerController._waitState);
            }
            else if(_playerController.currentDialogueCounter == 48) //켄타를 찾아서 옷장에서 나오는 부분
            {
                _playerController.ChangeState(_playerController._waitState);
                //대사가 다 끝난 경우, wait 상태에서 2초 정도 대기한 후, Fade in Fade out 연출로 거실에서 시작.
                //fade out 후에도 2초 정도 대기 시간이 있도록 ! 이후에는 바로 다음 대사 진행
            }
            else if(_playerController.currentDialogueCounter == 51)
            {
                _playerController.ChangeState(_playerController._waitState);
                _playerController._kimsinController.RunShowKimsinCoroutine();
                // 신야가 거실로 들어오면서 유우지가 신야를 바라봄.
                // 이후 79번 대사까지 출력 후 켄타를 거실 밖으로 이동시킴
                // 켄타가 정해진 위치까지 이동한 경우 80번 대사부터 출력 82번 대사가 출력되는 타이밍에 칸나도 거실 밖으로 이동시킴
                // 이후 89번까지 대사 출력
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
