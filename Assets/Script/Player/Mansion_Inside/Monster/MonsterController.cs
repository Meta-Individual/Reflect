using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource _stepAudioSource;
    public AudioClip[] StepSounds; // Carpet1, Carpet2, Carpet3, Carpet4


    public void PlayWalkingSound()
    {
        // 1부터 4까지의 숫자를 무작위로 선택합니다.
        int randomIndex = Random.Range(1, 5);

        // 선택된 숫자에 따라 해당하는 오디오 클립을 재생합니다.
        _stepAudioSource.clip = StepSounds[randomIndex];

        // 오디오를 재생합니다.
        _stepAudioSource.Play();
    }
}
