﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowDeadScene : MonoBehaviour, IInteractable
{
    [SerializeField] Image deadScene;
    [SerializeField] PlayerController _playerController;
    [SerializeField] float fadeDuration = 1f;  // 페이드 아웃 시간 (초)

    private void Start()
    {
        if(deadScene.gameObject.activeSelf)
        {
            deadScene.gameObject.SetActive(false);
        }
    }
    public void Interact()
    {
        StartCoroutine(ShowDeadSceneCoroutine());
    }

    IEnumerator ShowDeadSceneCoroutine()
    {
        _playerController.ChangeState(_playerController._waitState);
        FadeManager.Instance.StartFadeIn();
        yield return new WaitForSeconds(3.0f);
        FadeInImage();
        yield return new WaitForSeconds(1.5f);
        FadeManager.Instance.StartFadeOut();
        yield return new WaitForSeconds(6.0f);
        FadeOutImage();
        FadeManager.Instance.JustFade();
        yield return new WaitForSeconds(1.5f);
        FadeManager.Instance.StartFadeOut();
        _playerController.anim.Play("Shudder");

        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("ToBeContinue");
    }

    public void FadeInImage()
    {
        StartCoroutine(FadeInImageCoroutine(deadScene));
    }

    // 오브젝트의 Image 컴포넌트를 받아 투명도를 서서히 낮추는 함수
    public void FadeOutImage()
    {
        // Image 컴포넌트를 확인
        if (deadScene != null)
        {
            // Image의 투명도를 조절하는 코루틴 실행
            StartCoroutine(FadeOutImageCoroutine(deadScene));
        }
        else
        {
            Debug.LogWarning("Target object does not have an Image component.");
        }
    }

    // Image의 투명도를 천천히 줄이는 코루틴
    private IEnumerator FadeOutImageCoroutine(Image image)
    {
        Color color = image.color;
        float startAlpha = color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            image.color = color;
            yield return null;
        }

        deadScene.gameObject.SetActive(false);


        // 완전히 투명하게 설정
        color.a = 0;
        image.color = color;
    }

    private IEnumerator FadeInImageCoroutine(Image image)
    {
        deadScene.gameObject.SetActive(true);

        Color color = image.color;
        float startAlpha = color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(0, 1, normalizedTime);
            image.color = color;
            yield return null;
        }

        // 완전히 투명하게 설정
        color.a = 1;
        image.color = color;
    }
}
