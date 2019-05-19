﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private PlayerController controller;
    private new Camera camera;
    private GunController gunController;
    
    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        camera = Camera.main;
    }

    void Update()
    {
        //Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //Rotation input
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt(point);
        }
        
        //Weapon input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
