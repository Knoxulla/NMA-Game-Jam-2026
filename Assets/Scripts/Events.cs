using UnityEngine;
using System;
public class Events
{
    // Pause/Options Event
    public event Action OnPauseGame;
    public void PauseGame()
    {
        OnPauseGame?.Invoke();
    }

    // Timer Ended Event
    public event Action OnTimerEnded;
    public void ResolveTime()
    {
        OnTimerEnded?.Invoke();
    }

    // Update Score Event
    public event Action<int> OnScoreUpdated;
    public void UpdateScore(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    // Game Over Event
    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

   
    internal void ClearAll()
    {
        OnGameOver = null;
    }
}
