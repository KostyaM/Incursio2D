using System;
using UnityEngine;

public class EnimySpawner : MonoBehaviour
{
    public GameObject enimyPrefub;
    public long respawnIntervalMillis;
    private GameObject enimy;
    public GameObject stat;
    private EnimyProperty[] levelProperties = {
        new EnimyProperty(0.7f, 0.7f, 0.7f, 0.7f, 0.7f),
        new EnimyProperty(0.75f, 0.75f, 0.75f, 0.75f, 0.75f),
        new EnimyProperty(0.8f, 0.8f, 0.8f, 0.8f, 0.8f),
        new EnimyProperty(0.85f, 0.85f, 0.85f, 0.85f, 0.85f),
        new EnimyProperty(0.9f, 0.9f, 0.9f, 0.9f, 0.9f),
        new EnimyProperty(0.95f, 0.95f, 0.95f, 0.95f, 0.95f),
        new EnimyProperty(1f, 1f, 1f, 1f, 1f)
    };

    // Start is called before the first frame update
    void Start()
    {
        needRespawn = true;
    }

    // Update is called once per frame
    private long deathInMillis = 0;
    private bool needRespawn = true;
    void Update()
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (needRespawn && deathInMillis + respawnIntervalMillis <= now)
            respawn();
        if (enimy == null)
        {
            if (!needRespawn)
            {
                deathInMillis = now;
                stat.GetComponent<EnimyKillListener>().OnKill();
                GameApplication.GetInstance().OnKill();
            }
            needRespawn = true;
        }
    }

    private void respawn()
    {
        if (!needRespawn)
            return;
        enimy = Instantiate(enimyPrefub, transform.position, transform.rotation);
        enimy.GetComponent<EnimyComponentCabine>().enimyProperty = levelProperties[GameApplication.GetInstance().level];
        needRespawn = false;
    }
}
