using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel
    {
        Master,
        Sfx,
        Music
    };
    
    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    private AudioSource[] musicSources;
    private int activeMusicSourceIndex = 0;
    private Transform audioListener;
    public Transform playerT;
    private AudioLibrary library;
    private AudioSource sfx2DSource;

    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }
            
            GameObject newSfx2DSource = new GameObject("2D sfx source");
            sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
            newSfx2DSource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;
            
            library = GetComponent<AudioLibrary>();
            
            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (FindObjectOfType<Player>() != null)
        {
            playerT = FindObjectOfType<Player>().transform;
        }
    }
    
    void Update()
    {
        if (playerT != null)
        {
            audioListener.position = playerT.position;
        }
    }

    public void SetVolume(float volumePercent, AudioChannel audioChannel)
    {
        switch (audioChannel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip musicClip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = musicClip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCorssfade(fadeDuration));
    }
    
    public void PlaySound(AudioClip audioClip, Vector3 position)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, sfxVolumePercent * masterVolumePercent);
        }
    }

    public void PlaySound(string soundName, Vector3 position)
    {
        PlaySound(library.GetClipFromName(soundName), position);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName),sfxVolumePercent * masterVolumePercent);
    }
    
    IEnumerator AnimateMusicCorssfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }
    }

}
