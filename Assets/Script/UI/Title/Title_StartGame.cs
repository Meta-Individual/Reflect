using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_StartGame : MonoBehaviour 
{
    public string sceneName = "MansionOutScene_Opening";

    public void StartBtn() 
    {
        SceneManager.LoadScene(sceneName);
    } 
}