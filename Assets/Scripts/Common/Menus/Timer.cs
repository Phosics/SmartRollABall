using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float TimeInGame { get; private set; }
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    private void Start()
    {
        // Starts the timer automatically
        StartTimer();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            TimeInGame += Time.deltaTime;
            DisplayTime(TimeInGame);
        }
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void ResumeTimer()
    {
        timerIsRunning = true;
    }

    public void ResetTimer()
    {
        TimeInGame = 0;
    }

    public void StartTimer()
    {
        ResetTimer();
        ResumeTimer();
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}