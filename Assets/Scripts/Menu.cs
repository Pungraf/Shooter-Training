using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionMenuHolder;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWitdhs;

    private int activeScreenResIndex;

    private void Start()
    {
        mainMenuHolder.transform.localScale = Vector3.one;
        optionMenuHolder.transform.localScale = Vector3.zero;
        
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1 ) ? true : false;

        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        fullscreenToggle.isOn = isFullscreen;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        mainMenuHolder.transform.localScale = Vector3.zero;
        optionMenuHolder.transform.localScale = Vector3.one;
    }

    public void MainMenu()
    {
        mainMenuHolder.transform.localScale = Vector3.one;
        optionMenuHolder.transform.localScale = Vector3.zero;
    }

    public void SetScreenResolution(int number)
    {
        if (resolutionToggles[number].isOn)
        {
            activeScreenResIndex = number;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWitdhs[number], (int)(screenWitdhs[number]/aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void Fullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetVolume(volume, AudioManager.AudioChannel.Master);
    }
    
    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetVolume(volume, AudioManager.AudioChannel.Music);
    }
    
    public void SetSfxVolume(float volume)
    {
        AudioManager.instance.SetVolume(volume, AudioManager.AudioChannel.Sfx);
    }
}
