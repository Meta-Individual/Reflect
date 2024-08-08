using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // 대화창
    public PlayerController _playerController;

    void Start()
    {
        dialoguePanel = GameObject.FindGameObjectWithTag("Dialogue");
        dialoguePanel.SetActive(false);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    public void ShowDialogue(string character, string sprite, string dialogue)
    {
        _playerController.ChangeState(_playerController._waitState);
        StartCoroutine(ActiveDialogue(character, sprite, dialogue));
    }

    // Update is called once per frame
    private IEnumerator ActiveDialogue(string character, string spirte, string dialogue)
    {
        _playerController.charcter.sprite = LoadSprite(character, spirte); //캐릭터 이미지 불러오기
        _playerController.characterName.text = character; //캐릭터 이름 불러오기
        _playerController.dialogue.text = ""; // 텍스트 초기화
        _playerController.dialoguePanel.SetActive(true);

        foreach (char letter in dialogue)
        {
            _playerController.dialogue.text += letter;
            yield return new WaitForSeconds(0.05f); // 지연 시간 대기
        }
        _playerController.ChangeState(_playerController._diaState);

    }

    public Sprite LoadSprite(string folderName, string spriteName)
    {
        string path = "Illustration/" + folderName + "/" + spriteName;
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite != null)
        {
            Debug.Log("Sprite loaded successfully from " + path);
        }
        else
        {
            Debug.LogError("Sprite not found at " + path);
        }
        return sprite;
    }
}
