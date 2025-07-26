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
    [SerializeField] Slider fpsSlider;

    Resolution[] resolutions;
    Resolution[] newResolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        AddResolutionOptions();
        InitializeSettings();
    }

    public void SetFPSLimiter(float value)
    {
        int _fps = (int)value;
        Application.targetFrameRate = _fps;
        PlayerPrefs.SetInt("FPSLimit", _fps);
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

        Resolution resolution = newResolutions[resolutionindex];
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

    private bool IntToBool(int value)
    {
        return value > 0;
    }

    private void AddResolutionOptions()
    {
        //declaring lists and local variables
        List<string> _options = new List<string>();
        List<Resolution> _newResolutions = new List<Resolution>();
        string _option;
        int _currentResolutionIndex = 0;
        Resolution _resolution;
        //Filling up Lists
        for(int i=resolutions.Length-1; i>=0;i--)
        {
            _resolution = resolutions[i];
            _option = _resolution.width + " x " + _resolution.height;
            if (!_options.Contains(_option))
            {
                _options.Add(_option);
                _newResolutions.Add(_resolution);
            }
        }

    foreach(Resolution resolution in _newResolutions)
        {
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = _newResolutions.IndexOf(resolution);
                break;
            }
        }
        //Adding options and saving new, shorten Array of resolutions
        resolutionDropdown.AddOptions(_options);
        resolutionDropdown.SetValueWithoutNotify(_currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        newResolutions = _newResolutions.ToArray();
    }

    private void InitializeSettings()
    {

        //Resolution
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.RefreshShownValue();
        }
        SetResolution(resolutionDropdown.value);

        //Display Mode
        if (PlayerPrefs.HasKey("DisplayModeIndex"))
        {
            displayDropdown.value = PlayerPrefs.GetInt("DisplayModeIndex");
            displayDropdown.RefreshShownValue();
        }
        SetDisplayMode(displayDropdown.value);

        //FPS limiter
        if (PlayerPrefs.HasKey("FPSLimit"))
        {
            fpsSlider.value = (float)PlayerPrefs.GetInt("FPSLimit");
        }
        SetFPSLimiter(fpsSlider.value);

        //V-Sync
        if (PlayerPrefs.HasKey("Vsync"))
        {
            vSynchToggle.isOn = IntToBool(PlayerPrefs.GetInt("Vsync"));
        }
        SetVSync(vSynchToggle.isOn);

        //Anti-Aliasing
        if (PlayerPrefs.HasKey("AntiAliasing"))
        {
            antiAliasingToggle.isOn = IntToBool(PlayerPrefs.GetInt("AntiAliasing"));
        }
        SetAntiAliasing(antiAliasingToggle.isOn);
    }
}
