using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsinController : MonoBehaviour
{
    public Transform moveIntoLivingRoom;  // �̵��� ��� ������
    public Transform moveOut;
    public float moveSpeed = 15f;   // �̵� �ӵ�
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

    IEnumerator ShowKimsin() // ����� �ŽǱ��� �̵���Ű�� �÷��̾��� ������ �Ʒ��� ��ȯ �� ��� ���
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", 1.0f);
        while (transform.position != moveIntoLivingRoom.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveIntoLivingRoom.position, moveSpeed * Time.deltaTime);
            yield return null; // ���� �����ӱ��� ���
        }
        anim.SetBool("Walk", false);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", -1.0f);
        yield return new WaitForSeconds(1.0f);
        _playerController.maxDialogueCounter = 93;
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }

    IEnumerator MoveOutKimsin() // ����� �������� �̵���Ų �� ��Ȱ��ȭ
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveOut.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveOut.position, moveSpeed * Time.deltaTime);
            yield return null; // ���� �����ӱ��� ���
        }
        this.gameObject.SetActive(false);
        _playerInventory.AddItem("Outside");
        yield return new WaitForSeconds(1.0f);
    }
}
