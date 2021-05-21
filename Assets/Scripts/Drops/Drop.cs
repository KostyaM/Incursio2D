using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    public DropType type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            switch (type)
            {
                case DropType.HEALTH:
                    collision.collider.transform.GetComponent<DamageableComponent>().remainHealth += 10;
                    break;
                case DropType.BULLET:
                    collision.collider.transform.GetComponent<Weapon>().addBullets(5);
                    break;
                case DropType.HE:
                    collision.collider.transform.GetComponent<Weapon>().addHe(1);
                    break;
            }
            Destroy(gameObject);
        }
    }

    [SerializeField]
    public enum DropType
    {
        HEALTH,
        HE,
        BULLET
    }
}
