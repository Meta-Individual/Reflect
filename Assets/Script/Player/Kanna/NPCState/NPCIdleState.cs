using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : MonoBehaviour, INPCState
{
    private NPCController _npcController;

    public void OnStateEnter(NPCController npcController)
    {
        if (!_npcController)
            _npcController = npcController;
    }
    public void OnStateUpdate()
    {
        //Debug.Log("Kanna Idle");

        if (_npcController.Hide())
        {
            _npcController.ChangeState(_npcController._hideState);
        }

        if (!_npcController.isArrive)
        {
            _npcController.ChangeState(_npcController._walkState);
        }

    }

    public void OnStateExit()
    {

    }

}
