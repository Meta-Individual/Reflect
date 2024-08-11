using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KentaController : MonoBehaviour
{
    public Animator anim;
    public Transform moveOut;

    [HideInInspector]
    public PlayerController _playerController;

    [Header("Movement")]
    public float walkSpeed = 15f;    



    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void MoveKenta()
    {
        StartCoroutine(MoveOutKenta());
    }


    IEnumerator MoveOutKenta() // 김신을 거실까지 이동시키고 플레이어의 방향을 아래로 변환 후 대사 출력
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveOut.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveOut.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        this.gameObject.SetActive(false);
    }
}
