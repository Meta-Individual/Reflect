﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 문이 잠긴 소리 출력
 * 괴물 등장 사운드 출력
 * 김신 뒷걸음질
 * 괴물 등장
 * 대사 출력
 * 카메라 유우지로 포커스 전환
 * 유우지 저택 안으로 진입
 */
public class KimsinController2 : MonoBehaviour
{
    [Header("Animator")]
    public Animator _animator;
    [Header("Walk")]
    public float walkSpeed = 10f;
    [Header("MoveBackZone")]
    public Transform moveBack;
    [Header("Sound")]
    public AudioSource _audioSource;
    public AudioClip doorSound;

    public void PlayDoorSound() //잠긴 문 소리 출력
    {
        _audioSource.clip = doorSound;
        _audioSource.Play();
    }

    public void WalkingBack() //뒷걸음질 시작
    {
        StartCoroutine(StartMoveBack());
    }

    IEnumerator StartMoveBack()
    {
        _animator.SetBool("Walk", true);
        while (transform.position != moveBack.position) //그림자가 해당 위치로 이동할 때까지 대기
        {
            transform.position = Vector3.MoveTowards(transform.position, moveBack.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        _animator.SetBool("Walk", false);
    }
}
