using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Subject
{
    public enum State{Idle, Chasing, Attacking}

    private State currentState;
    
    private NavMeshAgent pathFinder;
    private Transform target;

    private float attackDistanceThreshold = .5f;
    private float timeBetweenAtacks = 1;

    private float nextAttackTime;
    private float myCollisionRadius;
    private float targetCollisionRadius;
    private Material skinMaterial;
    private Color originalColor;
    
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Chasing;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAtacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius/2);

        float attackSpeed = 3;
        float percent = 0;
        
        skinMaterial.color = Color.red;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
