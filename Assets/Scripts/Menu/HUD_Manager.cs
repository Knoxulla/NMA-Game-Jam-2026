using UnityEngine;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text score;

    [SerializeField] int currentQuota = 0;

    private void OnEnable()
    {
        GameManager.Instance.events.OnScoreUpdated += UpdateScoreDisplay;
        GameManager.Instance.events.OnQuotaSet += UpdateQuota;
    }

    private void OnDisable()
    {
        GameManager.Instance.events.OnScoreUpdated -= UpdateScoreDisplay;
        GameManager.Instance.events.OnQuotaSet -= UpdateQuota;
    }

    private void UpdateQuota(int newQuota)
    { 
        currentQuota = newQuota;
    }

    private void UpdateScoreDisplay(int newScore)
    {
        score.text = $"Quota: {GameManager.Instance.currentScore} / {currentQuota}";
    }
}
