using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{

    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target;

    public override void Start ()
    {
        base.Start ();
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        target = GameObject.FindGameObjectWithTag ("Tower").transform;

        StartCoroutine(UpdatePath());
    }

    // void Start()
    // {

    // }


    void Update()
    {

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (target != null)
        {
            Vector3 targetPosition = new Vector3 (target.position.x, 0, target.position.z);
            if (!dead)
            {
                pathFinder.SetDestination (target.position);
            }
           
            yield return new WaitForSeconds (refreshRate);
        }
    }
}
