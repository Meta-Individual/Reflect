using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHideState : MonoBehaviour, INPCState
{
    private NPCController _npcController;

    public void OnStateEnter(NPCController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

    }
    public void OnStateUpdate()
    {
        //Debug.Log("Kanna Hide");

        _npcController.SetTransparency();
    }

    public void OnStateExit()
    {

    }
}
