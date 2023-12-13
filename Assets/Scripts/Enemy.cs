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

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (target != null)
        {
            Vector3 targetPosition = new Vector3 (target.position.x, 0, target.position.z);
            pathFinder.SetDestination (target.position);
            yield return new WaitForSeconds (refreshRate);
        }
    }
}
