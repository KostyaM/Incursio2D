using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyCabineElementHit : DamageableComponent
{

    private void Start()
    {
        health = (int)(health * transform.parent.GetComponent<EnimyComponentCabine>().enimyProperty.Health);
        OnStart();
    }

    public override void DestroyElement(float delay)
    {
        transform.parent.GetComponent<EnimyComponentCabine>().DestroyElement();
    }
}
