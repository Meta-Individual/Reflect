using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMonologue : MonoBehaviour
{
    private static LoadMonologue _instance;
    public static LoadMonologue Instance { get { return _instance; } }

    private Dictionary<string, string> monologues = new Dictionary<string, string>();

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
                monologues[objectID] = monologue;
            }
        }
    }

    public string GetMonologue(string objectID)
    {
        if (monologues.TryGetValue(objectID, out string monologue))
        {
            return monologue;
        }
        return "이 물건에 대해 특별한 것은 없어 보여.";
    }
}
