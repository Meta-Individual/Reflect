using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ShowDialogue(string objectID)
    {
        StartCoroutine(ActiveDialogue(objectID));
    }

    // Update is called once per frame
    private IEnumerator ActiveDialogue(string objectID)
    {
        (string characterName, string sprite, string dialogue) = LoadDialogue.Instance.GetDialogue(objectID);

        _playerController.isDialogue = true;
        _playerController.ChangeState(_playerController._diaState);
        _playerController.charcter.sprite = LoadSprite(characterName, sprite); //캐릭터 이미지 불러오기
        _playerController.characterName.text = characterName; //캐릭터 이름 불러오기
        _playerController.dialogue.text = ""; // 텍스트 초기화
        _playerController.dialoguePanel.SetActive(true);
        _playerController.currentDialogueCounter++;

        for (int i = 0; i < dialogue.Length; i++) //대사 나오는 도중 Space바를 누르면 대사 스킵
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _playerController.dialogue.text = dialogue;
                break;
            }
            _playerController.dialogue.text += dialogue[i];
            yield return new WaitForSeconds(0.05f);

        }

        _playerController.isDialogue = false;
    }

    public Sprite LoadSprite(string folderName, string spriteName)
    {
        string path = "Illustration/" + folderName + "/" + spriteName;
        Debug.Log(path);
        Sprite sprite = Resources.Load<Sprite>(path);
        Debug.Log(sprite);
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
