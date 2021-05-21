using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApplication : MonoBehaviour, EnimyKillListener
{
    public int[] nextLevelConditions = { 1, 1, 1, 1, 1 };
    public int sessionMinutes = 20;

    public GameObject stat;
    public TMPro.TextMeshProUGUI countdownText;
    public GameObject countdownMenu;

    private int killCount = 0;
    public int level = -1;
    private float sessionLength = 0;
    private float countdown = 3;

    public bool isCountdownRunning = true;

    private static GameApplication instance;

    public void OnKill()
    {
        killCount++;
        if (level >= nextLevelConditions.Length) 
        {
            //proccede untill die
            NewLevel();
            return;
        }
        var nextLevelCondition = nextLevelConditions[level];
        if(killCount == nextLevelCondition)
        {
            NewLevel();
        }
    }

    public static GameApplication GetInstance()
    {
        return instance;
    }

    public void SetCountdown()
    {
        countdown = 3;
        countdownMenu.SetActive(true);
        isCountdownRunning = true;
    }

    void Start()
    {
        instance = this;
        sessionLength = sessionMinutes * 60 + 5;
        NewLevel();
    }

    private void Update()
    {
        
        if(sessionLength <= 0)
        {
            //TODO finish game
            return;
        }
        var timePassed = Time.deltaTime;
        sessionLength -= timePassed;
        countdown -= timePassed;
        countdownText.text = ((int)countdown + 1).ToString();
        if (countdown <= 0)
        {
            countdownMenu.SetActive(false);
            isCountdownRunning = false;
        }


        stat.GetComponent<GameStageListener>().OnTimerTick(
            (int)(sessionLength / 60),
            (int)(sessionLength % 60)
        );
    }

    private void NewLevel()
    {
        killCount = 0;
        level++;
        if(level > nextLevelConditions.Length)
        {
            stat.GetComponent<GameStageListener>().OnLevelChanged("INSANE");
            return;
        }
        stat.GetComponent<GameStageListener>().OnLevelChanged(level.ToString());
    }

}
