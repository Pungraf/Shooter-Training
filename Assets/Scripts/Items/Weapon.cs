using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
    public WeaponObject WeaponPrefab;
    
    public override void Use()
    {
        base.Use();
     //   EquipmentManager.instance.EquipWeaponObject(this);
        
    }
    
}
