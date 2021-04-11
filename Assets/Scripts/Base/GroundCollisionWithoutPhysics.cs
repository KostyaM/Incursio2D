using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionWithoutPhysics : GroundCollision
{

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), collision.transform.parent.GetComponent<Collider>(), true);
    }
}
