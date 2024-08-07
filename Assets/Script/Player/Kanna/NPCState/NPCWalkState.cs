using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPCWalkState : MonoBehaviour, INPCState
{
    private NPCController _npcController;
    private Vector2 movement;
    private Vector2 moveDirection;
    private int currentTargetIndex;
    private Rigidbody2D rb;

    public void OnStateEnter(NPCController npcController)
    {
        if(!_npcController)
            _npcController = npcController;

        _npcController.anim.SetBool("Walk", true);
        moveDirection = Vector2.zero;
        currentTargetIndex = 0;
        rb = _npcController._rigidbody;
    }


    public void OnStateUpdate()
    {
        if (_npcController)
        {
            //Debug.Log("Kanna Walk");

            /*Vector2 currentTarget = _npcController.targetPositions[currentTargetIndex]; // ���� Ÿ�� ��ġ
            Vector2 currentPosition = transform.position; // ���� �÷��̾� ��ġ
            Vector2 direction = (currentTarget - currentPosition).normalized;

            // x�� �̵� ���� �α� ���
            if (direction.x > 0)
            {
                _npcController.anim.SetFloat("DirX", 1.0f);
            }
            else if (direction.x < 0)
            {
                _npcController.anim.SetFloat("DirX", -1.0f);
            }
            else if (direction.y > 0)
            {
                _npcController.anim.SetFloat("DirY", 1.0f);
            }
            else if (direction.y < 0)
            {
                _npcController.anim.SetFloat("DirY", -1.0f);
            }

            // �̵�
            rb.MovePosition(currentPosition + direction * _npcController.walkSpeed * Time.fixedDeltaTime);

            // Ÿ�� ��ġ�� �����ߴ��� üũ
            if (Vector2.Distance(currentPosition, currentTarget) < 0.1f)
            {
                currentTargetIndex++; // ���� Ÿ������ �̵�
                if (currentTargetIndex >= _npcController.targetPositions.Count)
                {
                    _npcController.isArrive = true;
                    _npcController.ChangeState(_npcController._idleState);
                }
            }*/
        }
    }
    public void OnStateExit()
    {
        _npcController.anim.SetBool("Walk", false);
    }

}
