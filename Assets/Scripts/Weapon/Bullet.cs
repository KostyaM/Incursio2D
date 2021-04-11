using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;
    // Start is called before the first frame update
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enimyComponent =  collision.GetComponent<DamageableComponent>();
        if (enimyComponent != null)
        {
            enimyComponent.Damage(damage);
        }

       Destroy(gameObject);
    }

}
