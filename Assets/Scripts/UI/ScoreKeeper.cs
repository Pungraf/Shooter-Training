﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int score { get; private set; }
    private float lastEnemyKillTime;
    private int streakCount;
    private float StrikeExpireTime = 1;
    
    void Start()
    {
        Enemy.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    void Update()
    {
        
    }

    private void OnEnemyKilled()
    {
        if (Time.time < lastEnemyKillTime + StrikeExpireTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }

        lastEnemyKillTime = Time.time;

        score += 5 + (int)(Mathf.Pow(2, streakCount));
    }

    void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }
}
