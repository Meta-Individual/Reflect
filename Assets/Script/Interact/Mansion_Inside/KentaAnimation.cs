using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentaAnimation : MonoBehaviour, IInteractable
{
    public string itemName = "KentaInteract";
    public Direction currentDirection = Direction.UP;
    public int currentDialogueID = 47;
    public Animator anim;
    public GameObject kenta;
    public AudioSource _audioSource;
    public AudioClip kentaSound;

    private bool isSearched = false;
    private PlayerController _playerController;
    private PlayerInventory playerInventory;
    [SerializeField]
    private KannaController kannaController;


    void Start()
    {
        kenta.SetActive(false);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    public void Interact()
    {
        Debug.Log("Kenta 애니메이션 상호작용");
        if (!isSearched)
        {
            if (DirectionUtils.CheckDirection(currentDirection))
            {
                isSearched = true;
                playerInventory.AddItem(itemName);
                StartCoroutine(ActiveKentaAnimation());
            }
        }
    }

    IEnumerator ActiveKentaAnimation() //옷장의 흔들림을 멈추고, 켄타가 옷장에서 나오는 애니메이션 재생 이후 대사 출력
    {
        _playerController.ChangeState(_playerController._waitState);
        yield return new WaitForSeconds(1.0f);
        _playerController.MoveDownPlayer();
        kannaController.anim.SetFloat("DirX", -1.0f);
        kannaController.anim.SetFloat("DirY", 0.0f);
        anim.SetFloat("DirX", 1.0f);
        anim.SetFloat("DirY", 0.0f);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("Out", true);
        PlayKentaSound();
        yield return new WaitForSeconds(2.0f);
        _playerController.ChangeState(_playerController._diaState);
        _playerController.maxDialogueCounter = currentDialogueID;
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
        kenta.SetActive(true);
    }

    public void PlayKentaSound() //켄타 옷장 흔들리는 소리 재생
    {
        _audioSource.clip = kentaSound;
        _audioSource.Play();
    }
}
