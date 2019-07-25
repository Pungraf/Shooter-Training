using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;
    public override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (distance <= radius)
        {
            Debug.Log("Item Picked");
            bool wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
