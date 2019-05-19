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

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
