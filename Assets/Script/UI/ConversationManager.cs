using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConversationManager : MonoBehaviour
{
    public Canvas UI;
    public Text targetText;
    public string fullText = "아카네 아주머니는 잘 계세요 ?\n 아가는 어때요 ? 예뻐요 ?";
    public float delay = 0.1f; // 각 글자 사이의 지연 시간

    private bool conversationFlag = false;


    void Update()
    {
        if (conversationFlag)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(ActivateCanvas());
            }
        }
    }

    private IEnumerator ActivateCanvas()
    {
        targetText.text = ""; // 텍스트 초기화

        UI.gameObject.SetActive(true);
        foreach (char letter in fullText.ToCharArray())
        {
            targetText.text += letter;
            yield return new WaitForSeconds(delay); // 지연 시간 대기
        }
    }

    private void InactiveCanvas()
    {
        UI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("conversation In");
            conversationFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("conversation Out");
            InactiveCanvas();
            conversationFlag = false;
        }
    }
}
