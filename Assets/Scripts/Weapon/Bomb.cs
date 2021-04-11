using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float fieldOfImpact;
    public float force;
    public long delay;
    public int damage;
    public AudioClip blustSound;

    public GameObject explosionEffect;


    private long explodeMillis = -1;
    private void Start()
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        explodeMillis = now + delay;
        Debug.Log("Explode " + explodeMillis);
    }

    // Update is called once per frame
    void Update()
    {

        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (explodeMillis != -1 && explodeMillis <= now)
            Explode();
    }

    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact);
        foreach (Collider2D obj in objects)
        {
            applyForceToObj(obj.gameObject);
        }
        var position = transform.position;
        var rotation = transform.rotation;
        Destroy(gameObject);
        var effect = Instantiate(explosionEffect, position, rotation);
        effect.GetComponent<AudioSource>().PlayOneShot(blustSound);
        Destroy(effect, 10);
    }

    private void applyForceToObj(GameObject obj)
    {
        var rigibody = obj.gameObject.GetComponent<Rigidbody2D>();
        if (rigibody == null)
        {
            var parent = obj.transform.parent;
            if (parent != null)
                applyForceToObj(parent.gameObject);
        }

        var distanceToObj = Mathf.Pow(Mathf.Pow(obj.transform.position.x - transform.position.x, 2) + Mathf.Pow(obj.transform.position.y - transform.position.y, 2), 0.5f);     
        var distanceСoefficient = (fieldOfImpact - distanceToObj) / fieldOfImpact;

        if (rigibody != null)
        {
            var direction = obj.transform.position - transform.position;
            rigibody.AddForce(direction * (force * distanceСoefficient));
        }
        
        var damageListener = obj.GetComponent<DamageableComponent>();
        if (damageListener != null)
        {
            damageListener.Damage((int)(damage * distanceСoefficient), 0.5f);           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
