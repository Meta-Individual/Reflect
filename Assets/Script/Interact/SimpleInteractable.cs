using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    public GameObject player; // 플레이어
    private Transform _playerTransform;
    private Vector3 offset = new(0, 15f, 0); // 말풍선의 위치 오프셋
    public PlayerController _playerController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
        _playerController = player.GetComponent<PlayerController>();
    }

    public void Interact()
    {
        string dialogue = DialogueManager.Instance.GetDialogue(objectID);
        StartCoroutine(ActiveMonologue(dialogue));
    }

    private IEnumerator ActiveMonologue(string message)
    {
        _playerController.ChangeState(_playerController._waitState);
        _playerController.monologuePanel.SetActive(true);
        _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = ""; // 텍스트 초기화
        _playerController.monologuePanel.transform.position = _playerTransform.position + offset;

        foreach (char letter in message)
        {
            _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.05f); // 지연 시간 대기
        }
        _playerController.ChangeState(_playerController._monoState);
    }
}
