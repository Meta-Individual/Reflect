﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowManual : MonoBehaviour
{
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private Image manual;
    [SerializeField] private float fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    [SerializeField] private FirstDialogue firstDialogue;
    private PlayerController _playerController;



    void Start()
    {
        manualPanel.SetActive(false);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (_playerController.mansionInside == 0)
        {
            StartCoroutine(ShowManualCoroutine());
        }
    }

    IEnumerator ShowManualCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        manualPanel.SetActive(true);
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeIn()
    {
        // 페이드 인 로직
        manual.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1, false)); // 페이드 인 완료 대기
    }

    private IEnumerator FadeOut()
    {
        // 페이드 아웃 로직
        manual.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1, 0, true)); // 페이드 아웃 완료 대기
        manual.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        firstDialogue.ShowDialogue();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, bool deactivateOnEnd)
    {
        manual.gameObject.SetActive(true);

        Color color = manual.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            manual.color = color;
            yield return null;
        }

        color.a = endAlpha;
        manual.color = color;

        if (deactivateOnEnd)
        {
            manual.gameObject.SetActive(false); // 투명해지면 비활성화
        }

    }

    private IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(1.0f); // 1초 대기
        yield return StartCoroutine(FadeIn()); // 페이드 인 효과 시작 및 완료 대기
        yield return new WaitForSeconds(3.0f); // 추가 대기 시간
        yield return StartCoroutine(FadeOut()); // 페이드 아웃 효과 시작 및 완료 대기
    }
}
