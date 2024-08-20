using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public List<Resolution> supportedResolutions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSupportedResolutions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSupportedResolutions()
    {
        supportedResolutions = Screen.resolutions
            .Select(resolution => new Resolution(resolution.width, resolution.height))
            .Distinct()
            .OrderByDescending(r => r.width * r.height)
            .ToList();
    }

    public void SetResolution(int index)
    {
        Resolution res = supportedResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
