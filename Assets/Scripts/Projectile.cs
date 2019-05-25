using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public Color trailColor;
    private float speed = 10f;
    private float damage = 1;

    private float lifeTime = 10;
    private float skinWidth = .1f;
    private static readonly int TintColor = Shader.PropertyToID("_TintColor");

    void Start()
    {
        //TODO Switch for Object Pool
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
        
        GetComponent<TrailRenderer>().material.SetColor(TintColor, trailColor);
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }
    
    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        Destroy(gameObject);
    }
}
