using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    public SpriteRenderer dot;
    public LayerMask targetMask;
    public Color onTargetColor;
    private Color originalDotColor;
    private float rotationSpeed;
    
    void Start()
    {
        originalDotColor = dot.color;
        Cursor.visible = false;
    }
    
    void Update()
    {
        transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
    }

    public void DetectTarget(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            dot.color = onTargetColor;
            rotationSpeed = 160;
        }
        else
        {
            dot.color = originalDotColor;
            rotationSpeed = 40;
        }
    }
}
