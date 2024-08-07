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
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/ObjectMonologue");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 2)
            {
                string objectID = values[1].Trim();
                string sceneName = values[2]; // Remove quotes
                string objectName = values[4];
                string monologue = values[5]; 
                //Debug.Log(objectID + sceneName + objectName + monologue);
                dialogues[objectID] = monologue;
            }
        }
    }

    public string GetDialogue(string objectID)
    {
        if (dialogues.TryGetValue(objectID, out string dialogue))
        {
            return dialogue;
        }
        return "이 물건에 대해 특별한 것은 없어 보여.";
    }
}
