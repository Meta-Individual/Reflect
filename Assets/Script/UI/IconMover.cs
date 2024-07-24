using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMover : MonoBehaviour
{
    public RectTransform iconTransform; // �̵��� �������� RectTransform
    public float speed = 5.0f; // �������� �̵��ϴ� �ӵ�
    public float height = 50.0f; // �������� �̵��� ����

    private Vector2 originalPosition; // �������� ���� ��ġ
    private bool movingUp = true; // �������� ���� �̵� ������ ����

    void Start()
    {
        if (iconTransform == null)
        {
            iconTransform = GetComponent<RectTransform>();
        }
        originalPosition = iconTransform.anchoredPosition;
    }

    void Update()
    {
        float newY = iconTransform.anchoredPosition.y + (movingUp ? speed : -speed) * Time.deltaTime;

        if (movingUp && newY > originalPosition.y + height)
        {
            newY = originalPosition.y + height;
            movingUp = false;
        }
        else if (!movingUp && newY < originalPosition.y)
        {
            newY = originalPosition.y;
            movingUp = true;
        }

        iconTransform.anchoredPosition = new Vector2(iconTransform.anchoredPosition.x, newY);
    }
}
