using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    private SpriteRenderer targetRenderer; // 타겟 오브젝트의 SpriteRenderer
    private SpriteRenderer playerRenderer; // 플레이어 오브젝트의 SpriteRenderer
    private Animator playerAnim; // 플레이어 오브젝트의 Rigidbody2D
    private GameObject player;

    void Start()
    {
        // 타겟 오브젝트의 SpriteRenderer 컴포넌트를 가져옵니다.
        targetRenderer = GetComponent<SpriteRenderer>();

        // "Player" 태그를 가진 오브젝트의 SpriteRenderer와 Rigidbody2D 컴포넌트를 가져옵니다.
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRenderer = player.GetComponent<SpriteRenderer>();
            playerAnim = player.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인하세요.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerRenderer == null || playerAnim == null) return;

        if (other.CompareTag("Player"))
        {
            //Debug.Log("바운드 진입");
            //Debug.Log(playerAnim.GetFloat("DirY"));
            // 플레이어의 이동 방향이 아래쪽인지 위쪽인지 확인합니다.
            if (playerAnim.GetFloat("DirY") < 0)
            {
                //플레이어가 아래 방향으로 이동 중이면
                //Debug.Log("아래 방향");
                targetRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
            }
            else if (playerAnim.GetFloat("DirY") > 0)
            {
                //플레이어가 위 방향으로 이동 중이면
                //Debug.Log("위 방향");
                targetRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (playerRenderer == null || playerAnim == null) return;

        if (other.CompareTag("Player"))
        {
            //Debug.Log("바운드 아웃");
            //Debug.Log(playerAnim.GetFloat("DirY"));
            // 플레이어의 이동 방향이 아래쪽인지 위쪽인지 확인합니다.
            if (playerAnim.GetFloat("DirY") < 0)
            {
                //플레이어가 아래 방향으로 이동 중이면
                //Debug.Log("아래 방향");
                targetRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
            }
            else if (playerAnim.GetFloat("DirY") > 0)
            {
                //플레이어가 위 방향으로 이동 중이면
                //Debug.Log("위 방향");
                targetRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
            }
        }
    }
}
