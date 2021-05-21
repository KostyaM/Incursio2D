using UnityEngine;

public class EnimyComponentCabine : MonoBehaviour, DistanceCheckListener, CollisionListener
{

    public EnimyProperty enimyProperty = new EnimyProperty(1f, 1f, 1f, 1f, 1f);

    public float speed;
    public float chaseDistance;
    public GameObject[] drops;
    public GameObject destroyEffect;

    private Transform target;
    private Vector2 homePosition;
    private bool isWorking = true;
    private bool isMinDistanceReached = false;


    public void DestroyElement()
    {
        isWorking = false;
        dropItem();
        Destroy(Instantiate(destroyEffect, transform.position, transform.rotation), 4);
        Destroy(gameObject);
    }

    private void dropItem()
    {
        var dropPrefab = drops[Random.Range(0, drops.Length)];
        Destroy(Instantiate(dropPrefab, transform.position, transform.rotation), 20);
    }

    public void onMinDistance()
    {
        if(isWorking)
            isMinDistanceReached = true;
    }

    public void onMaxDistance()
    {
        if (isWorking)
            isMinDistanceReached = false;
    }

    public void onInsaneDistance()
    {
        DestroyElement();
    }


    public void onCollide()
    {
        DestroyElement();
    }


    public void onExitCollide()
    {

    }

    private void Start()
    {
        homePosition = transform.position;
        var objects = GameObject.FindGameObjectsWithTag("Player");
        if (objects.Length != 0)
            target = objects[0].transform;
    }

    // Update is called once per frame
    private float previousYVelocity = 0f;
  
    void Update()
    {
        if (Menu.isGamePaused)
            return;
        if (isMinDistanceReached)
        {
            var velocity = GetComponent<Rigidbody2D>().velocity.y;
            float impulse = 0f;
           
            if (velocity < 0)
            {

                impulse = Mathf.Abs((previousYVelocity - velocity < 0 ? velocity : 0) / 1.5f);
                previousYVelocity = velocity;

            }
            else 
            {
                impulse = Mathf.Abs(((velocity - previousYVelocity < 0 ? velocity : 0) / 1.5f));
                previousYVelocity = velocity;
            }
           
            impulse += 0.1f;

            var engine1 = transform.GetChild(0).GetComponent<EnimyComponentEngine>();
            var engine2 = transform.GetChild(1).GetComponent<EnimyComponentEngine>();
            if (engine1 != null)
                engine1.MakeImpulse(impulse);
            if (engine2 != null)
                engine2.MakeImpulse(impulse);

        }
        Chase();
               
            
            
    }

    private void Chase()
    {
        if (target == null)
        {
            ReturnHome();
            return;
        }
        var toTargetXCathet = Mathf.Abs(transform.position.x - target.position.x);
        var toTargetYCathet = Mathf.Abs(transform.position.y - target.position.y);

        var distanceToTarget = Mathf.Pow(toTargetXCathet * toTargetXCathet + toTargetYCathet * toTargetYCathet, 0.5f);
        if(distanceToTarget > (chaseDistance * enimyProperty.ChaseDistance))
        {
            ReturnHome();
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, 0f), speed * Time.deltaTime * enimyProperty.ChaseSpeed);
    }

    private void ReturnHome()
    {
        if (CheckIfTargetReached(homePosition))
            return;        

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(homePosition.x, 0f), speed * Time.deltaTime * enimyProperty.ChaseSpeed);
    }

    private bool CheckIfTargetReached(Vector2 target)
    {
        var currentPosX = transform.position.x;
        var targetPosX = target.x;
        if (Mathf.Abs(targetPosX - currentPosX) < 0.5)
            return true;
        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
