using UnityEngine;

public class Stat : MonoBehaviour, HealthListener, WeaponListener, EnimyKillListener, GameStageListener
{

    public static int killCount = 0;
    public TMPro.TextMeshProUGUI healthPreview, bulletPreview, hePreview, killPreview, levelPreview, timer;

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
        if(value < 0)
        {
            healthPreview.text = 0.ToString();
            return;
        }
        healthPreview.text = value.ToString();
    }

    public void OnTimerTick(int minutes, int seconds)
    {
        timer.text = string.Format("{0}:{1}", GetPrettyNumbers(minutes), GetPrettyNumbers(seconds));
    }

    public void OnLevelChanged(string level)
    {
        levelPreview.text = string.Format("LVL {0}", level);
    }

    private string GetPrettyNumbers(int number)
    {   
        if (number > 9)
            return number.ToString();
        return string.Format("0{0}", number);
    }
}
