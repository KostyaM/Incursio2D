using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public GameObject listener;
    private GameObject surface;
    public float minAltitude;
    public float maxAltitude;
    public float insaneDistance;


    private void Start()
    {
        surface = GameObject.FindGameObjectWithTag("Ground");
    }
    // Update is called once per frame
    void Update()
    {
        if (surface == null)
            return;
        var distance = Physics2D.Distance(surface.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>()).distance;
        if (distance < minAltitude)
        {
            listener.GetComponent<DistanceCheckListener>().onMinDistance();
        } else if(distance > maxAltitude){
            listener.GetComponent<DistanceCheckListener>().onMaxDistance();
        }

        if(distance > insaneDistance)
            listener.GetComponent<DistanceCheckListener>().onInsaneDistance();
        if(distance < 0.02)
            listener.GetComponent<CollisionListener>().onCollide();
    }
}
