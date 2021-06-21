using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;
        // just to be safe
        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        resolutionDropdown.AddOptions(
            resolutions.Select((res, i) => {
            if (Screen.currentResolution.width == resolutions[i].width &&
            Screen.currentResolution.height == resolutions[i].height)
            {
                currentResIndex = i;
            }
            return res.width + " x " + res.height;
        }).ToList());

        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void setResolution(int i)
    {
        Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreen);
    }

    public void setVolume(float volume)
    {
        // UnityEngine.Debug.Log($"volume: {volume}");
        audioMixer.SetFloat("Volume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        UnityEngine.Debug.Log($"{qualityIndex}");
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
