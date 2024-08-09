using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOutOfDeskState : MonoBehaviour, INPCState
{
    private NPCController _npcController;
    private PlayerController pc;
    private float animationTimer = 3f;
    private float startTimer;

    public void OnStateEnter(NPCController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

        pc = _npcController._playerController;
        pc.ChangeState(pc._waitState);
        StartCoroutine(StartAnimationSet());
        startTimer = 0;
    }
    public void OnStateUpdate()
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
        _npcController.chair_desk_Move.MoveCoroutine();
        yield return new WaitForSeconds(1f);
        _npcController.RecoverTransparency();
        _npcController.anim.SetBool("Out", true);

    }
}
