using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody thisRb;
    private new Camera camera;
    public Interactable focus;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Focus();
    }

    private void FixedUpdate()
    {
        thisRb.MovePosition(thisRb.position + velocity * Time.fixedDeltaTime);
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightOffsetPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightOffsetPoint);
    }
    
    //Items focus
    
    private void Focus()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                RemoveFocus();
            }
        } 
		
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocus();
            }

            focus = newFocus;
        }
        focus.OnFocused(transform);
        focus.Interact();
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocus();
        }

        focus = null;
    }
}
