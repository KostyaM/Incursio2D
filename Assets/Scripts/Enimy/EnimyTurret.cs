using System;
using UnityEngine;

public class EnimyTurret : DamageableComponent
{
    public float range;
    private Transform target;
 
    Vector2 direction;
    public GameObject weapon;
    public GameObject bullet;
    public Transform bulletPoint;
    public float Force;
    public long shootDelayMillis;
    public GameObject shootEffectPrefab;
    public AudioClip fireSound;


    private bool isWorking = true;
    public override void DestroyElement(float delay)
    {
        isWorking = false;
        Destroy(gameObject, delay);
    }

    private void Start()
    {
        health = (int)(health * transform.parent.GetComponent<EnimyComponentCabine>().enimyProperty.Health);
        var objects = GameObject.FindGameObjectsWithTag("Player");
        if (objects.Length != 0)
            target = objects[0].transform;
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.isGamePaused)
            return;

        if (!isWorking || target == null)
            return;

        var parentTransform = bulletPoint.transform.position;
        var objects = Physics2D.OverlapCircleAll(parentTransform, range);
        GameObject player = null;
        foreach (Collider2D obj in objects)
        {
            if (obj.tag == "Player")
            {
                player = obj.gameObject;
                break;
            }
        }
           
        if (player != null)
        {
            direction = (Vector2)player.transform.position - (Vector2)transform.position;
            weapon.transform.right = direction;
            Shoot();
        }
    }

    private long lastFireMillis = 0;
    void Shoot()
    {
        if (GameApplication.GetInstance().isCountdownRunning)
            return;
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (lastFireMillis + shootDelayMillis > now)
            return;
        GameObject BulletIns = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        gameObject.GetComponent<AudioSource>().PlayOneShot(fireSound);
        var shootEffect = Instantiate(shootEffectPrefab, bulletPoint.position, bulletPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * Force, ForceMode2D.Impulse);
        lastFireMillis = now;
        Destroy(shootEffect, 0.2f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(bulletPoint.transform.position, range);
    }
}
