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
        _npcController.anim.SetFloat("DirY", -1.0f);
    }

    public void OnStateExit()
    {

    }

}
