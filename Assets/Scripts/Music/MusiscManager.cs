using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusiscManager : MonoBehaviour
{
    public AudioClip mainThem;
    public AudioClip menuThem;

    private string sceneName;
    void Start()
    {
        OnLevelWasLoaded(0);
    }
    

    void OnLevelWasLoaded(int sceneIndex)
    {
        string newSceneName = SceneManager.GetActiveScene().name;

        if (newSceneName != sceneName)
        {
            sceneName = newSceneName;
            Invoke(nameof(PlayMusic), .2f);
        }
    }

    void PlayMusic()
    {
        AudioClip clipToplay = null;

        if (sceneName == "Menu")
        {
            clipToplay = menuThem;
        }
        else if (sceneName == "Game")
        {
            clipToplay = mainThem;
        }

        if (clipToplay != null)
        {
            AudioManager.instance.PlayMusic(clipToplay, 2);
            Invoke(nameof(PlayMusic), clipToplay.length);
        }
    }
}
