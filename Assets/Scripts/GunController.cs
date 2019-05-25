using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform WeaponHold;
    public Gun StartingGun;
    private Gun equippedGun;

    void Start()
    {
        if (StartingGun != null)
        {
            EquipGun(StartingGun );
        }
    }

    void Update()
    {
        
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, WeaponHold.position, WeaponHold.rotation);
        equippedGun.transform.parent = WeaponHold;
    }

    public void OnTriggerHold()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerHold();
        }
    }

    public void OnTriggerRelease()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerReleased();
        }
    }

    public void Aim(Vector3 aimPoint)
    {
        if (equippedGun != null)
        {
            equippedGun.Aim(aimPoint);
        }
    }

    public void Reload()
    {
        if (equippedGun != null)
        {
            equippedGun.Reload();
        }
    }

    public float GunHeight => WeaponHold.position.y;
}
