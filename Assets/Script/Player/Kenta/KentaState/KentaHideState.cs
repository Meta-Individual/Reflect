using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentaHideState : MonoBehaviour, IKentaState
{
    private KentaController _npcController;

    public void OnStateEnter(KentaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

    }
    public void OnStateUpdate()
    {
        //Debug.Log("Kenta Hide");

        _npcController.SetTransparency();
    }

    public void OnStateExit()
    {

    }
}
