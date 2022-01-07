using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    // Public Attributes
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public TMP_Dropdown resolutionDropdown;

    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public GameObject mainMenuGameObject;

    // Private Attributes
    private Resolution[] resolutions;
    private MainMenu mainMenu;

    private void Start()
    {
        mainMenu = mainMenuGameObject.GetComponent<MainMenu>();

        // Get aviable resolutions & clear dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Add aviable resolutions to a list
        int currentResolutionIndex = 0;
        List<string> resOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add aviable resolutions to dropdown
        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume (float volume)
    {
        masterMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume (float volume)
    {
        masterMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume (float volume)
    {
        masterMixer.SetFloat("sfxVolume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ClickedBackButton()
    {
        mainMenuGameObject.SetActive(true);
        mainMenu.ResetRespawns();

        gameObject.SetActive(false);
    }
}
