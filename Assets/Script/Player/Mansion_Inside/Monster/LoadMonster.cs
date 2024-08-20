using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMonster : MonoBehaviour
{
    public PlayerController controller;
    public Transform movePoint;
    public Transform moveBackPoint;

    void Start() //저택 내부 씬에 접근할 때 플레이어 inspector 창에 대입
    {
        controller = FindObjectOfType<PlayerController>();

        controller.monster = this.gameObject;
        controller.monster_MovePoint = movePoint;
        controller.monster_MoveBackPoint = moveBackPoint;
    }
}
