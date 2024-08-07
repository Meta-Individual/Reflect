using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    public GameObject dialoguePanel; // 말풍선 패널
    public Transform player; // 플레이어 Transform
    private Vector3 offset = new Vector3(0, 15f, 0); // 말풍선의 위치 오프셋

    void Start()
    {
        dialoguePanel = GameObject.FindGameObjectWithTag("Monologue");
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public void Interact()
    {
        string dialogue = DialogueManager.Instance.GetDialogue(objectID);
        StartCoroutine(ActiveMonologue(dialogue));
        //ShowDialogue(dialogue);
    }

    private void ShowDialogue(string message)
    {
        Debug.Log(message); // 실제 게임에서는 UI로 표시
    }

    private IEnumerator ActiveMonologue(string message)
    {
        dialoguePanel.SetActive(true);
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = ""; // 텍스트 초기화
        dialoguePanel.transform.position = player.position + offset;

        foreach (char letter in message)
        {
            dialoguePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.05f); // 지연 시간 대기
        }
    }
}
