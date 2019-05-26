using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float masterVolumePercent = .2f;
    public float sfxVolumePercent = 1f;
    public float musicVolumePercent = 1f;

    private AudioSource[] musicSources;
    private int activeMusicSourceIndex = 0;
    private Transform audioListener;
    private Transform playerT;

    public static AudioManager instance;
    
    private void Awake()
    {
        instance = this;
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music source " + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        audioListener = FindObjectOfType<AudioListener>().transform;
        playerT = FindObjectOfType<Player>().transform;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (playerT != null)
        {
            audioListener.position = playerT.position;
        }
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
