using UnityEngine;

public abstract class DamageableComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthBarObj;
    public int health, remainHealth;
    public bool isDestroyed = false;


    protected void OnStart()
    {
        if (healthBarObj != null)
        {
            var healthBar = healthBarObj.GetComponent<HealthListener>();
            healthBar.setHealth(remainHealth, health);
        }
        Debug.Log("Health " + health);
    }

    public void Damage(int damage, float delay = 0f)
    {
        remainHealth = remainHealth - damage;
        if (healthBarObj != null)
        {
            var healthBar = healthBarObj.GetComponent<HealthListener>();
            healthBar.setHealth(remainHealth, health);
        }
       
        if (remainHealth <= 0)
        {
            remainHealth = 0;
            isDestroyed = true;
            DestroyElement(delay);
        }
    }

    public abstract void DestroyElement(float delay = 0f);

   
}
