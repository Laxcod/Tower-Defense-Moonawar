using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    NavMeshAgent pathFinder;
    Transform target;

    public enum State { Idle, Chasing, Attacking};
    State currentState;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttack = 1;

    float nextAttackTime;

    Material skinMaterial;
    Color originalColor;

    float myCollisionRadius;
    float targetCollisionRadius;

    public ParticleSystem deathEffect;
    Image healthBar;
    float fill = 1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        pathFinder = GetComponent<NavMeshAgent>();
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Tower").transform;

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<SphereCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow (attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttack;
                StartCoroutine(Attack());
            }

            if(sqrDistanceToTarget < 3)
            {
                healthBar.fillAmount -= .05f;
            }
        }
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    IEnumerator  UpdatePath()
    {
        float refreshRate = .25f;
        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition =  target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathFinder.SetDestination(target.position);
                }
            }
            yield return new WaitForSeconds (refreshRate);
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalposition = transform.position;

        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.green;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalposition, attackPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColor;

        currentState = State.Chasing;
        pathFinder.enabled = true;
    }
}