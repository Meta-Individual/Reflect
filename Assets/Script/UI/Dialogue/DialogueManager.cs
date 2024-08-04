using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get { return _instance; } }

    private Dictionary<string, string> dialogues = new Dictionary<string, string>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadDialogues();
        }
    }

    private void LoadDialogues()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("ObjectDialogues");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 2)
            {
                string objectID = values[0].Trim();
                string dialogue = values[1].Trim().Trim('"'); // Remove quotes
                dialogues[objectID] = dialogue;
            }
        }
    }

    public string GetDialogue(string objectID)
    {
        if (dialogues.TryGetValue(objectID, out string dialogue))
        {
            return dialogue;
        }
        return "�� ���ǿ� ���� Ư���� ���� ���� ����.";
    }
}