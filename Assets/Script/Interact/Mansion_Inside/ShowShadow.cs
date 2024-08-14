using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShadow : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController; //이벤트가 발생할 때 플레이어가 못 움직이게 만들기위해 선언
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject shadow; //그림자 오브젝트
    [SerializeField]
    private Transform shadowTransform; //그림자가 움직일 위치
    [SerializeField]
    private float walkSpeed = 30f;
    private PlayerInventory inventory;
    private MonologueManager _monologueManager;


    public Direction direction; //사용자의 현재 방향
    void Start() // 게임시작후 그림자 오브젝트는 비활성화
    {
        inventory = FindObjectOfType<PlayerInventory>();
        _monologueManager = FindObjectOfType<MonologueManager>();
        shadow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어 ShowShadow 진입");
            if (DirectionUtils.CheckDirection(direction) ) //플레이어의 방향이 스크립트에서 설정한 값과 일치하고 해당 아이템을 가지고 있다면 코루틴 실행
            {
                Debug.Log("방향 일치");
                if (inventory.HasItem("LibraryDoorKey"))
                {
                    Debug.Log("플레이어 방향 일치 + 아이템 소지");
                    StartCoroutine(ShowShadowWithSound());
                }

            }
        }
    }

    IEnumerator ShowShadowWithSound()
    {
        shadow.SetActive(true);
        _playerController.ChangeState(_playerController._waitState);

        while (shadow.transform.position != shadowTransform.position) //그림자가 해당 위치로 이동할 때까지 대기
        {
            shadow.transform.position = Vector3.MoveTowards(shadow.transform.position, shadowTransform.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        shadow.SetActive(false);

        _playerController.anim.SetFloat("DirX", 0.0f); //그림자가 나타난 방향으로 플레이어의 몸 회전
        _playerController.anim.SetFloat("DirY", 1.0f);

        yield return new WaitForSeconds(1.0f);

        _monologueManager.ShowMonologue("92"); //화원 대사
        yield return null;
    }
}
