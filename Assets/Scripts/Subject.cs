﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    public System.Action OnDeath;
    
    protected virtual void  Start()
    {
        health = startingHealth;
    }

    void Update()
    {
        
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        //TODO for future use
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }
}
