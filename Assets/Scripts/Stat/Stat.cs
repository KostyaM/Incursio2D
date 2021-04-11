using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour, HealthListener, WeaponListener, EnimyKillListener
{

    public static int killCount = 0;
    public TMPro.TextMeshProUGUI healthPreview, bulletPreview, hePreview, killPreview;

    private void Start()
    {
        killPreview.text = killCount.ToString();
    }

    public void onBulletChange(int bulletCount)
    {
        bulletPreview.text = bulletCount.ToString();
    }

    public void onHeChange(int heCount)
    {
        hePreview.text = heCount.ToString();
    }

    public void OnKill()
    {
        killCount++;
        killPreview.text = killCount.ToString();
    }

    public void setHealth(int value, int full)
    {
        healthPreview.text =  value.ToString();
    }

}
