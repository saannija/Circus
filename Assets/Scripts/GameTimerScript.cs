using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime = 0f;
    private bool isPaused = false;
    private bool gameFinished = false;

    void Update()
    {
        if (!isPaused && !gameFinished)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void PauseTimer(bool pause)
    {
        isPaused = pause;
    }

    public void StopTimer()
    {
        gameFinished = true;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
