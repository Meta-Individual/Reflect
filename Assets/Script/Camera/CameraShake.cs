using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 흔들림 지속 시간
    public float shakeDuration = 0.5f;

    // 흔들림 강도
    public float shakeMagnitude = 0.1f;

    // 감쇠 속도
    public float dampingSpeed = 1.0f;

    // 초기 카메라 위치
    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        // 흔들림을 시작
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            shakeMagnitude = Mathf.Lerp(shakeMagnitude, 0, elapsed / shakeDuration);
            yield return null;
        }

        // 카메라 위치를 원래대로 되돌림
        transform.localPosition = initialPosition;
    }
}
