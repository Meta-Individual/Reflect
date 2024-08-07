using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour
{
    private static LoadDialogue _instance;
    public static LoadDialogue Instance { get { return _instance; } }

    private Dictionary<string, (string, string)> dialogues = new Dictionary<string, (string, string)>();

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
            LoadMonologues();
        }
    }

    private void LoadMonologues()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/CharacterDialogue");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 2)
            {
                string objectID = values[1].Trim();
                string sceneName = values[2]; // Remove quotes
                string spriteName = values[4];
                string dialogue = values[5];
                //Debug.Log(objectID + sceneName + spriteName + dialogue);
                dialogues[objectID] = (spriteName, dialogue);
            }
        }
    }
}
