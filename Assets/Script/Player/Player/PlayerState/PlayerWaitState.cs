using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("Wait", true);
        if (_playerController.currentDialogueCounter == 48) //��Ÿ�� ã�Ƽ� ���忡�� ������ �κ�
        {
            StartCoroutine(GotoLivingRoom());
        }
    }
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Wait", false);
    }

    public IEnumerator GotoLivingRoom() //��Ÿ�� ã�� �� ���̵��� ���̵�ƿ� ȿ���� �ŽǷ� ���� ��ȯ
    {
        FadeManager.Instance.StartFade(); // ���̵� �� �� 1�� ��� �� ���̵� �ƿ�
        yield return new WaitForSeconds(2.5f);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", 1.0f);
        _playerController.transform.position = new(0.0f, -388f, 0.0f);
        yield return new WaitForSeconds(2.0f);
        _playerController.ChangeState(_playerController._diaState);
        _playerController.maxDialogueCounter = 50; //���׸� �ž߰� �ŽǷ� ������ �κ�
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
        yield return null;
    }
}
