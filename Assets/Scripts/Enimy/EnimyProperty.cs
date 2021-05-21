using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyProperty
{
    private float accuracy;
    private float damage;
    private float chaseSpeed;
    private float chaseDistance;
    private float health;

    public EnimyProperty(float accuracy, float damage, float chaseSpeed, float chaseDistance, float health)
    {
        this.chaseDistance = chaseDistance;
        this.accuracy = accuracy;
        this.damage = damage;
        this.chaseSpeed = chaseSpeed;
        this.health = health;
    }

    public float Accuracy { get => accuracy; set => accuracy = value; }
    public float Damage { get => damage; set => damage = value; }
    public float ChaseSpeed { get => chaseSpeed; set => chaseSpeed = value; }
    public float Health { get => health; set => health = value; }
    public float ChaseDistance { get => chaseDistance; set => chaseDistance = value; }
}
