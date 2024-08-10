using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentaOutState : MonoBehaviour, IKentaState
{
    private KentaController _npcController;
    private PlayerController pc;
    private float animationTimer = 3f;
    private float startTimer;

    public void OnStateEnter(KentaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

        pc = _npcController._playerController;
        pc.ChangeState(pc._waitState);
        StartCoroutine(StartAnimationSet());
        startTimer = 0;
    }
    public void OnStateUpdate() // animation 시간에 도달한 경우 idleState로 전환하고 플레이어를 DialogueState로 전환
    {
        startTimer += Time.deltaTime;
        if(startTimer > animationTimer) 
        {
            _npcController.ChangeState(_npcController._idleState);
            pc.ChangeState(pc._diaState);
            pc.maxDialogueCounter = 20;
            pc._dialogueManager.ShowDialogue(pc.currentDialogueCounter.ToString());
        }
    }

    public void OnStateExit()
    {
        _npcController.anim.SetBool("Out", false);
    }


    IEnumerator StartAnimationSet()
    {
        yield return new WaitForSeconds(1f);
        _npcController.RecoverTransparency();
        _npcController.anim.SetBool("Out", true);
    }
}
