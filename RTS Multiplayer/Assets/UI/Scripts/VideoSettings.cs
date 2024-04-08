using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown displayDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle vSynchToggle;
    [SerializeField] Toggle antiAliasingToggle;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        AddResolutionOptions();
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            LoadAllSettings();
        }
        else
        {
            SetAllSettings();
        }
    }

    public void SetVSync(bool isOn)
    {
        if (isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        PlayerPrefs.SetInt("Vsync", QualitySettings.vSyncCount);
    }

    public void SetAntiAliasing(bool isOn)
    {
        if (isOn)
            QualitySettings.antiAliasing = 8;
        else
            QualitySettings.antiAliasing = 0;
        PlayerPrefs.SetInt("AntiAliasing", QualitySettings.antiAliasing);
    }

    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionindex);
    }

    public void SetDisplayMode(int displayModeIndex)
    {
        switch (displayModeIndex)
        {
            case 0:
                {
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                }
                break;
            case 1:
                {
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                }
                break;
            case 2:
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
                break;
        }
        PlayerPrefs.SetInt("DisplayModeIndex",displayModeIndex);
    }

    bool IntToBool(int value)
    {
        return value > 0;
    }

    void LoadAllSettings()
    {
        displayDropdown.value = PlayerPrefs.GetInt("DisplayModeIndex");
        displayDropdown.RefreshShownValue();
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        resolutionDropdown.RefreshShownValue();
        vSynchToggle.isOn = IntToBool(PlayerPrefs.GetInt("Vsync"));
        antiAliasingToggle.isOn = IntToBool(PlayerPrefs.GetInt("AntiAliasing"));

        SetAllSettings();
    }

    void SetAllSettings()
    {
        SetVSync(vSynchToggle.isOn);
        SetAntiAliasing(antiAliasingToggle.isOn);
        SetDisplayMode(displayDropdown.value);
        SetResolution(resolutionDropdown.value);
    }

    void AddResolutionOptions()
    {
        List<string> _options = new List<string>();
        string _option;
        int _currentResolutionIndex = 0;
        Resolution _resolution;
        for(int i=0;i<resolutions.Length;i++)
        {
            _resolution = resolutions[i];
            _option = _resolution.width + " x " + _resolution.height;
            _options.Add(_option);
            if(_resolution.width == Screen.currentResolution.width && _resolution.height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(_options);
        resolutionDropdown.value = _currentResolutionIndex;
    }
}