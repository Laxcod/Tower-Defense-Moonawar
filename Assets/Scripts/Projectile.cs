using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10;
    float damage = 1;
    public LayerMask collisionMask;

    float lifetime = 3;

   void Start()
   {
        Destroy (gameObject, lifetime);

        Collider[] initialCollision = Physics.OverlapSphere (transform.position, .1f, collisionMask);
        if (initialCollision.Length > 0)
        {
            OnHitObject (initialCollision [0], transform.position);
        }
   } 

   public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate (Vector3.forward * moveDistance); 
    }

    void CheckCollision (float moveDistance)
    {
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject (hit.collider, hit.point);
        }
    }
    
    void OnHitObject(Collider c, Vector3 hitPoint)
    {
      IDamageable damageableObject = c.GetComponent<IDamageable> ();
      if (damageableObject !=null)
      {
        damageableObject.TakeHit (damage, hitPoint, transform.forward);
      }
        GameObject.Destroy (gameObject);
    }
}
