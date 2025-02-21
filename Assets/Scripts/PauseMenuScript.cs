using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject buttonGroup;
    public GameObject settingsButtons;
    public GameObject leaderboardButtons;
    private bool isPaused = false;
    private GameTimer gameTimer;

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        pauseMenuUI.SetActive(false);
        settingsButtons.SetActive(false);
        leaderboardButtons.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsButtons.activeSelf || leaderboardButtons.activeSelf)
            {
                CloseSettings();
                CloseLeaderboard();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        gameTimer.PauseTimer(isPaused);
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        buttonGroup.SetActive(false);
        settingsButtons.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        buttonGroup.SetActive(false);
        leaderboardButtons.SetActive(true);
    }

    public void CloseSettings()
    {
        buttonGroup.SetActive(true);
        settingsButtons.SetActive(false);
    }

    public void CloseLeaderboard()
    {
        buttonGroup.SetActive(true);
        leaderboardButtons.SetActive(false);
    }
}
