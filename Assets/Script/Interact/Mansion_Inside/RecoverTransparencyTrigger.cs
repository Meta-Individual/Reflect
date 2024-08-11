using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverTransparencyTrigger : MonoBehaviour
{
    public GameObject targetObject; // 투명도를 변경할 오브젝트
    public GameObject targetObject2;
    private Renderer targetRenderer;
    private Renderer targetRenderer2;

    private void Start()
    {
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            targetRenderer2 = targetObject2.GetComponent<Renderer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetRenderer != null) // 플레이어가 트리거에 진입했을 때
        {
            SetTransparency(targetRenderer, 1f);
            SetTransparency(targetRenderer2, 1f);
        }
    }

    private void SetTransparency(Renderer render, float alpha)
    {
        Color color = render.material.color;
        color.a = alpha;
        render.material.color = color;
    }
}
