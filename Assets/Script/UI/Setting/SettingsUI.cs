using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in SettingsManager.Instance.supportedResolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);

        // ���� �ػ󵵿� ���� ����� �ɼ� ����
        Resolution currentResolution = new Resolution(Screen.width, Screen.height);
        int currentResolutionIndex = SettingsManager.Instance.supportedResolutions.FindIndex(r => r.width == currentResolution.width && r.height == currentResolution.height);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void OnResolutionChanged(int index)
    {
        SettingsManager.Instance.SetResolution(index);
    }

    private void OnFullscreenChanged(bool isFullscreen)
    {
        SettingsManager.Instance.SetFullscreen(isFullscreen);
    }
}
