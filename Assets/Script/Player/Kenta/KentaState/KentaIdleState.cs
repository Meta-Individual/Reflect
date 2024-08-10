using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentaIdleState : MonoBehaviour, IKentaState
{
    private KentaController _npcController;
    private PlayerController pc;

    public void OnStateEnter(KentaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;
        pc = _npcController._playerController;
    }
    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {

    }

}
