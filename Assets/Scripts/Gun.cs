using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireMode
    {
        Auto,
        Single
    };

    public FireMode fireMode;

    public Transform[] projectileSpawn;
    public Projectile projectile;
    public float fireCooldown = 100f;
    public float muzzleVelocity = 35f;

    private float nextShotTime;
    private bool triggerReleased;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            if (fireMode == FireMode.Single)
            {
                if (!triggerReleased)
                {
                    return;
                }
            }

            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                nextShotTime = Time.time + fireCooldown / 1000;
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation);
                newProjectile.SetSpeed(muzzleVelocity);
            }
        }
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleased = false;
    }

    public void OnTriggerReleased()
    {
        triggerReleased = true;
    }
}
