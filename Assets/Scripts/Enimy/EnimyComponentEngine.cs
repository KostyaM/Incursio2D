using UnityEngine;

public class EnimyComponentEngine : DamageableComponent
{

    public GameObject firePrefab;

    private bool isWorking = true;
   
    public override void DestroyElement(float delay)
    {
        isWorking = false;
        Destroy(gameObject, delay);    
    }

    private void Start()
    {
        health = (int)(health * transform.parent.GetComponent<EnimyComponentCabine>().enimyProperty.Health);
        OnStart();
    }


    public void MakeImpulse(float power) {
        if (!isWorking)
            return;
        var spaceShip = transform.parent.GetComponent<Rigidbody2D>();
        var angel = (90 + transform.eulerAngles.z) * Mathf.Deg2Rad;
        var x = power * Mathf.Cos(angel);
        var y = power * Mathf.Sin(angel);
        spaceShip.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }

}
