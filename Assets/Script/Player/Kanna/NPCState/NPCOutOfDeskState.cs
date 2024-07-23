using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOutOfDeskState : MonoBehaviour, INPCState
{
    private NPCController _npcController;

    private float animationTimer = 1.5f;
    private float startTimer;

    public void OnStateEnter(NPCController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

        _npcController.anim.SetBool("Out", true);
        startTimer = 0;
    }
    public void OnStateUpdate()
    {
        Debug.Log("Kanna OutofDesk");

        startTimer += Time.deltaTime;
        if(startTimer > animationTimer) 
        {
            _npcController.ChangeState(_npcController._idleState);
        }
    }

    public void OnStateExit()
    {
        _npcController.anim.SetBool("Out", false);
    }
}
