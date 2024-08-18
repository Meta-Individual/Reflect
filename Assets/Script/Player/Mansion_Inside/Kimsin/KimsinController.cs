using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsinController : MonoBehaviour
{
    public Transform moveIntoLivingRoom;  // 이동할 경로 지점들
    public Transform moveOut;
    public float moveSpeed = 15f;   // 이동 속도
    public Animator anim;
    private PlayerController _playerController;
    [SerializeField]
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.gameObject.SetActive(false);
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
        anim.SetBool("Walk", false);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", -1.0f);
        yield return new WaitForSeconds(1.0f);
        _playerController.maxDialogueCounter = 93;
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
