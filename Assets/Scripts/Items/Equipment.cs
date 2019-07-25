using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public override void Use()
    {
        base.Use();
     //   EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {RangedWeapon, MeleeWeapon, Server, Mask, Core, Coat, Pants}
public enum EquipmentMeshRegion {Legs, Torso, Arms}