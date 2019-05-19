using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody thisRb;
    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
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
}
