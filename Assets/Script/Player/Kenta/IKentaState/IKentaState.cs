using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKentaState
{
    void OnStateEnter(KentaController controller);
    void OnStateUpdate();
    void OnStateExit();
}
