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

        _npcController.anim.SetBool("Hide", true);
    }
    public void OnStateUpdate()
    {
        Debug.Log("Kanna Hide");

        _npcController.SetTransparency();
        
        if(_npcController.OutOfDesk())
        {
            _npcController.RecoverTransparency();
            _npcController.ChangeState(_npcController._outState);
        }
    }

    public void OnStateExit()
    {
        _npcController.anim.SetBool("Hide", false);
    }
}
