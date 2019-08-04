using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqSlot : MonoBehaviour
{
    public Image icon;
    public Equipment equipment;


    public void AddItem(Equipment newItem)
    {
        equipment = newItem;
        icon.sprite = equipment.icon;
        icon.enabled = true;
    }
    
    public void ClearSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    
    
    public void Unequip()
    {
        if (equipment != null)
        {
            ClearSlot();
        }
    } 
    
    public void UseItem()
    {
        if (equipment != null)
        {
            EquipmentManager.instance.Unequip(equipment);
            EquipmentManager.instance.UninstantiateEquipment(equipment);
            ClearSlot();
        }
    } 
}
