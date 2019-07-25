using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    public bool isFocused = false;

    public Transform player;
    public Transform interactionTransform;


    private void Awake()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
    }

    public void OnDefocus()
    {
        isFocused = false;
        player = null;
    }

    public virtual void Interact()
    {
        if (isFocused)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Debug.Log("Interact");
            }
        }
    }
}
