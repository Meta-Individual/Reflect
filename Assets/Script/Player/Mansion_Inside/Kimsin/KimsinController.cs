using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsinController : MonoBehaviour
{
    public Transform moveIntoLivingRoom;  // 이동할 경로 지점들
    public Transform moveOut;
    public float moveSpeed = 15f;   // 이동 속도
    public Animator anim;

    [Header("Controller")]
    public PlayerController _playerController;
    public KentaController _kentaController;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [Header("Movement")]
    public StepType stepType;

    [Header("Sound")]
    public AudioSource _stepAudioSource;
    public AudioClip[] CarpetClips; // Carpet1, Carpet2, Carpet3, Carpet4
    public AudioClip[] DirtClips;   // Dirt1, Dirt2, Dirt3, Dirt4
    public AudioClip[] HallClips;   // Hall1, Hall2, Hall3, Hall4
    public AudioClip[] ConcreteClips; // Concrete1, Concrete2, Concrete3, Concrete4
    public AudioClip[] GrassClips;  // Grass1, Grass2, Grass3, Grass4
    public AudioClip[] WoodClips;   // Wood1, Wood2, Wood3, Wood4
    private System.Random random = new();
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayWalkingSound()
    {
        AudioClip[] selectedClips = null;

        switch (stepType)
        {
            case StepType.Carpet:
                selectedClips = CarpetClips;
                break;
            case StepType.Dirt:
                selectedClips = DirtClips;
                break;
            case StepType.Hall:
                selectedClips = HallClips;
                break;
            case StepType.Concrete:
                selectedClips = ConcreteClips;
                break;
            case StepType.Grass:
                selectedClips = GrassClips;
                break;
            case StepType.Wood:
                selectedClips = WoodClips;
                break;
        }

        if (selectedClips != null && selectedClips.Length > 0)
        {
            int randomIndex = random.Next(0, selectedClips.Length);
            _stepAudioSource.clip = selectedClips[randomIndex];
            _stepAudioSource.Play();
        }
    }
    public void RunShowKimsinCoroutine()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ShowKimsin());
    }

    public void RunMoveOutCoroutine()
    {
        StartCoroutine(MoveOutKimsin());
    }

    IEnumerator ShowKimsin() // 김신을 거실까지 이동시키고 플레이어의 방향을 아래로 변환 후 대사 출력
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", 1.0f);
        while (transform.position != moveIntoLivingRoom.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveIntoLivingRoom.position, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        _kentaController.anim.SetFloat("DirY", -1.0f);

        anim.SetBool("Walk", false);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", -1.0f);
        yield return new WaitForSeconds(1.0f);
        _playerController.maxDialogueCounter = 88; //유우카한테 가볼까? 라는 대사까지
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }

    IEnumerator MoveOutKimsin() // 김신을 현관까지 이동시킨 후 비활성화
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveOut.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveOut.position, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        this.gameObject.SetActive(false);
        _playerInventory.AddItem("Outside");
        yield return new WaitForSeconds(1.0f);
    }
}
