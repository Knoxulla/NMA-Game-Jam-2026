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

    public event Action OnScoreReset;
    public void ResetScore()
    {
        OnScoreReset?.Invoke();
    }

    // Quota Set events
    public event Action<int> OnQuotaSet;
    public void SetQuota(int newQuota)
    {
        OnQuotaSet?.Invoke(newQuota);
    }


    // Game Over Event
    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    // Volume Events
    public event Action<float> OnMasterVolChanged;
    public void ChangeMasterVol(float newVol)
    {
        OnMasterVolChanged?.Invoke(newVol);
    }

    public event Action<float> OnSFXVolChanged;
    public void ChangeSFXVol(float newVol)
    {
        OnSFXVolChanged?.Invoke(newVol);
    }

    public event Action<float> OnMusicVolChanged;
    public void ChangeMusicVol(float newVol)
    {
        OnMusicVolChanged?.Invoke(newVol);
    }

}
