using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{

    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target;

    public enum State{idle, Chasing, Attacking};
    State CurrentState;

    float attackDistanceThresHold = .5f;
    float timeBetweenAttack = 1;

    float nextAttackTime;

    Material skinMaterial;
    Color originalColor;

    float myCollsionRadius;
    float targetCollisionRadius;

    public override void Start ()
    {
        base.Start ();
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        skinMaterial = GetComponent<Renderer> ().material;
        originalColor = skinMaterial.color;

        CurrentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag ("Tower").transform;

        myCollsionRadius = GetComponent<CapsuleCollider> ().radius;
        targetCollisionRadius = target.GetComponent<SphereCollider> ().radius;
        
        StartCoroutine(UpdatePath());
    }

    IEnumerator Attack()
    {
        CurrentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalPosition = transform.position;

        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollsionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.green;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow (percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp (originalPosition, attackPosition ,interpolation);

            yield return null;
        }
        skinMaterial.color = originalColor;

        CurrentState = State.Chasing;
        pathFinder.enabled = true;
    }

    // void Start()
    // {

    // }


    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow (attackDistanceThresHold + myCollsionRadius + targetCollisionRadius, 2))
            nextAttackTime = Time.time + timeBetweenAttack;
            StartCoroutine (Attack ());
        }
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (target != null)
        {
            if (CurrentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollsionRadius + targetCollisionRadius + attackDistanceThresHold/2);
            if (!dead)
                {
                    pathFinder.SetDestination (target.position);
                }    
            }
            yield return new WaitForSeconds (refreshRate);
        }
    }
}
