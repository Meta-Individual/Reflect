using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologueManager: MonoBehaviour
{
    public GameObject player; // �÷��̾�
    private Transform _playerTransform;
    private Vector3 offset = new(0, 15f, 0); // ��ǳ���� ��ġ ������
    public PlayerController _playerController;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
        _playerController = player.GetComponent<PlayerController>();
    }

    public void ShowMonologue(string monologue)
    {
        StartCoroutine(ActiveMonologue(monologue));
    }

    // Update is called once per frame
    private IEnumerator ActiveMonologue(string message)
    {
        _playerController.ChangeState(_playerController._waitState);
        _playerController.monologuePanel.SetActive(true);
        _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = ""; // �ؽ�Ʈ �ʱ�ȭ
        _playerController.monologuePanel.transform.position = _playerTransform.position + offset;

        foreach (char letter in message)
        {
            _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.05f); // ���� �ð� ���
        }
        _playerController.ChangeState(_playerController._monoState);
    }
}
