using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public SkinnedMeshRenderer targetMesh;
    public Equipment[] currentEquipment;
    private SkinnedMeshRenderer[] currentMesh;
    private Inventory inventory;
    public EquipmentObject equippedGun;
    private GunController _gunController;
    
    //Weapons holds
    private Transform weaponHold;

    public EquipmentObject MeleeWeapon;
    public EquipmentObject RangedWeaapon;
    public EquipmentObject Server;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentchanged;

    public delegate void OnEquipmentEquiped();
    public OnEquipmentEquiped onEquipmentEquiped;
    
    private void Start()
    {
        _gunController = GameManager.instance.Player.GetComponent<GunController>();
        weaponHold = _gunController.WeaponHold;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        inventory = Inventory.instance;
        currentEquipment = new Equipment[numSlots];
        currentMesh = new SkinnedMeshRenderer[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.equipSlot;
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        
        InstantaiateEquipment(newItem);
        
        currentEquipment[slotIndex] = newItem;
        

        if (onEquipmentchanged != null)
        {
            onEquipmentchanged.Invoke(newItem, oldItem);
        }
        if (onEquipmentEquiped != null)
        {
            onEquipmentEquiped.Invoke();
        }
        
    }

    public void Unequip(Equipment equipment)
    {
        int slotIndex = (int) equipment.equipSlot;
        
        if (currentEquipment[slotIndex] != null)
        {
            currentEquipment[slotIndex] = null;
            inventory.Add(equipment);
        }
    }

    /*public void RenderArmor(Armor newArmor)
    {
        int slotIndex = (int) newArmor.equipSlot;
        
        SetEquipmentBlendShapes(newArmor, 100);
        
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newArmor.mesh, targetMesh.transform, true);

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMesh[slotIndex] = newMesh;
    }*/

    /*public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMesh[slotIndex] != null)
            {
                Destroy(currentMesh[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            //TODO if for armors
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            if (currentEquipment[slotIndex].GetType() == typeof(Weapon))
            {
                UnequipWeaponObject((Weapon) currentEquipment[slotIndex]);
            }

            currentEquipment[slotIndex] = null;
            
            if (onEquipmentchanged != null)
            {
                onEquipmentchanged.Invoke(null, oldItem);
            }
        }
        
    }*/

    /*public void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }*/

    public void InstantaiateEquipment(Equipment item)
    {
        EquipmentSlot equipmentSlot = item.equipSlot;
        switch (equipmentSlot)
        {
            case EquipmentSlot.RangedWeapon:
                RangedWeaapon = item.EquipmentPrefab;
                if (equippedGun != null)
                {
                    Destroy(equippedGun.gameObject);
                }
                equippedGun = Instantiate(item.EquipmentPrefab, weaponHold.position, weaponHold.rotation);
                equippedGun.transform.parent = weaponHold;
                _gunController.equippedGun = (Gun) equippedGun;
                break;
            case EquipmentSlot.MeleeWeapon:
                MeleeWeapon = item.EquipmentPrefab;
                break;
            case EquipmentSlot.Server:
                Server = item.EquipmentPrefab;
                break;
        }
    }
    
    public void UninstantiateEquipment(Equipment item)
    {
        EquipmentSlot equipmentSlot = item.equipSlot;
        switch (equipmentSlot)
        {
            case EquipmentSlot.RangedWeapon:
                if (equippedGun != null)
                {
                    Destroy(equippedGun.gameObject);
                }
                RangedWeaapon = null;
                break;
            case EquipmentSlot.MeleeWeapon:
                MeleeWeapon = null;
                break;
            case EquipmentSlot.Server:
                Server = null;
                break;
        }
    }

}
