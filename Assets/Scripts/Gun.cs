using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Gun : MonoBehaviour
{
    public enum FireMode
    {
        Auto,
        Single
    };

    public FireMode fireMode;

    public Transform[] projectileSpawn;
    public GameObject projectile;
    public float fireCooldown = 100f;
    public float muzzleVelocity = 35f;
    public int magCapacity;
    public float reloadSpeed = .3f;
    public Queue<GameObject> bulletPool;

    
    [Header("Recoil")]
    public Vector2 kickMinMax = new Vector2(.05f, .2f);
    public Vector2 recoilAngleMinMax = new Vector2(3, 5);
    public float recoilMovementSettleTime = .1f;
    public float recoilRotationSettleTime = .1f;

    [Header("Effects")]
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private float nextShotTime;
    private bool triggerReleased;
    private int projectileInMag;
    private bool isReloading;
    
    private Vector3 recoilSmoothDampVelocity;
    private float recoilAngle;
    private float recoilAngleSmoothDampVelocity;
    
    void Start()
    {
        projectileInMag = magCapacity;
        PoolManager.instance.CreatePool(projectile, 50);
    }

    void LateUpdate()
    {
        // Animate recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilMovementSettleTime);
        recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilAngleSmoothDampVelocity, recoilRotationSettleTime);
        transform.localEulerAngles += Vector3.left * recoilAngle;

        if (!isReloading && projectileInMag <= 0)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (!isReloading && Time.time > nextShotTime && projectileInMag > 0)
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
                if (projectileInMag == 0)
                {
                    break;
                }
                projectileInMag--;
                nextShotTime = Time.time + fireCooldown / 1000;
                PoolManager.instance.RespawnObject(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation, muzzleVelocity);
                //newProjectile.SetSpeed(muzzleVelocity);
                transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x, kickMinMax.y);
                recoilAngle += Random.Range(recoilAngleMinMax.x, recoilAngleMinMax.y);
                recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);
            }
            
            AudioManager.instance.PlaySound(shootSound, transform.position);
        }
    }

    public void Reload()
    {
        if (!isReloading && projectileInMag != magCapacity)
        {
            StartCoroutine(AnimateReload());
            AudioManager.instance.PlaySound(reloadSound, transform.position);
        }
    }

    IEnumerator AnimateReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(.2f);

        float reloadSpeed = 1f / this.reloadSpeed;
        float percent = 0;
        Vector3 initialRot = transform.localEulerAngles;
        float maxReloadAngle = 30;

        while (percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);

            transform.localEulerAngles = initialRot + Vector3.left * reloadAngle;

            yield return null;
        }

        isReloading = false;
        projectileInMag = magCapacity;
    }

    public void Aim(Vector3 aimPoint)
    {
        if (!isReloading)
        {
            transform.LookAt(aimPoint);
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
