using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    public void Move (Vector3 _velocity) 
    {
        velocity = _velocity;
    }
    public void FixedUpdate()
    {
        rb.MovePosition (rb.position + velocity *Time.fixedDeltaTime);
    }
}
