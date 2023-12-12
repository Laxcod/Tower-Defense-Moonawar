using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target;

    void Start()
    {
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        target = GameObject.FindGameObjectWithTag ("Tower").transform;
    }


    void Update()
    {
        pathFinder.SetDestination (target.position);
    }
}
