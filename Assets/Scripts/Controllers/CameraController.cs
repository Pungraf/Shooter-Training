using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.3f;
    public float heigh = 6f;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 pos = new Vector3();
            pos.x = player.position.x;
            pos.z = player.position.z - 2f;
            pos.y = player.position.y + heigh;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        }

    }
}
