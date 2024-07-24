using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    public float rotationSpeed = 100f; // ������Ʈ�� ȸ�� �ӵ�
    private float moveSpeed = 1f;       // ������Ʈ�� �̵� �ӵ�
    private float moveDistance = 0.5f;    // ������Ʈ�� �̵��� �ִ� �Ÿ�

    private Vector3 startPosition;
    private float moveDirection = 1f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Y���� �߽����� ȸ��
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Y������ �̵�
        transform.position += new Vector3(0, moveDirection * moveSpeed * Time.deltaTime, 0);

        // �ִ� �̵� �Ÿ� �ʰ� �� �̵� ���� ����
        if (Mathf.Abs(transform.position.y - startPosition.y) > moveDistance)
        {
            moveDirection *= -1;
        }
    }
}
