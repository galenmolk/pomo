using System;
using TMPro;
using UnityEngine;

public class PomodoroTimer : MonoBehaviour
{
    private const double POMODORO_INTERVAL = 1500d;
    
    [SerializeField] private TMP_Text timerText;

    private bool hasStarted;
    private double secondsRemaining;
    private DateTime startTime;
    
    public void StartTimer()
    {
        startTime = DateTime.Now;
        secondsRemaining = POMODORO_INTERVAL;
        UpdateText();
        hasStarted = true;
    }

    public void ResetTimer()
    {
        hasStarted = false;
        secondsRemaining = 0;
        UpdateText();
    }

    private void Awake()
    {
        UpdateText();
    }

    private void Update()
    {
        if (!hasStarted)
            return;

        TickTime();
        UpdateText();
        CheckTime();
    }

    private void TickTime()
    {
        secondsRemaining = POMODORO_INTERVAL - DateTime.Now.Subtract(startTime).TotalSeconds;
    }
    
    private void UpdateText()
    {
        timerText.text = ConvertSecondsToMinutesString(secondsRemaining);
    }

    private void CheckTime()
    {
        if (secondsRemaining <= 0d)
            ResetTimer();
    }
    
    private string ConvertSecondsToMinutesString(double timeInSeconds)
    {
        TimeSpan formatTime = TimeSpan.FromSeconds(timeInSeconds);
        return $"{formatTime.Minutes:00}:{formatTime.Seconds:00}";
    }
}
