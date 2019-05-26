using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image fadePlane;
    public GameObject gameOverUI;

    public RectTransform newWaveBanner;
    public Text newWaveTitle;
    public Text newWaveEnemyCount;

    private Spawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    void Start()
    {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
    }

    void Update()
    {
        
    }

    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true);
    }

    void OnNewWave(int waveNumber)
    {
        string[] numbers = {"One", "Two", "Three", "Four", "Five"};
        newWaveTitle.text = "- Wave " + numbers[waveNumber - 1] + " -";
        newWaveEnemyCount.text = "Enemies: " + spawner.waves[waveNumber - 1].enemyCount;

        StartCoroutine(AnimateNewWaveBanner());
    }

    IEnumerator AnimateNewWaveBanner()
    {
        float delay = 1f;
        float percent = 0f;
        float speed = 2.5f;
        int dir = 1;

        float endDelayTime = Time.time + delay + 1/delay;

        while (percent >= 0)
        {
            percent += Time.deltaTime * speed * dir;

            if (percent >= 1)
            {
                percent = 1;
                if (Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }
            
            newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-70, 140, percent);
            yield return null;
        }
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }
    
    //UI Input

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    } 
}
