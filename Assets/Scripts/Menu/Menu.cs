using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, DefeatListener
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUi;
    public GameObject defeatMenuUi;
    public TextMeshProUGUI score;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (defeatMenuUi.active == true)
                return;
            if(isGamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }


    public void onDefeat()
    {
        defeatMenuUi.SetActive(true);
        score.text = "Your score " + Stat.killCount.ToString();
    }


    public void Pause()
    {
        AudioListener.volume = 0.1f;
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        AudioListener.volume = 1f;
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        GameApplication.GetInstance().SetCountdown();
    }

    public void Restart()
    {
        Stat.killCount = 0;
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(sceneIndex);
        isGamePaused = false;
        Time.timeScale = 1f;
        GameApplication.GetInstance().SetCountdown();
        SceneManager.LoadScene(sceneIndex);
    }

    public void BackToMenu()
    {
        Stat.killCount = 0;
        SceneManager.UnloadSceneAsync("GameScene");
        isGamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Stat.killCount = 0;
        Application.Quit();
    }
}
