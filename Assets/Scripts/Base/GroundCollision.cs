using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{

    public GameObject collidableObject;
    private bool inDeathZone = false;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            collidableObject.GetComponent<CollisionListener>().onCollide();
        }
        if (collision.collider.tag == "DeathZone")
        {
            inDeathZone = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            collidableObject.GetComponent<CollisionListener>().onExitCollide();
        }
        if(collision.collider.tag == "DeathZone")
        {
            inDeathZone = false;
        }

    }

    private void Update()
    {
        if(inDeathZone)
            collidableObject.GetComponent<DamageableComponent>().Damage(1);
    }
}
