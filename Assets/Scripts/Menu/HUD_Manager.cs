using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] public Slider timerSlider;

    public int currentQuota = 0;

    private void OnEnable()
    {
        GameManager.Instance.events.OnQuotaSet += UpdateQuotaDisplay;
        GameManager.Instance.events.OnScoreUpdated += UpdateScoreDisplay;

    }

    private void OnDisable()
    {
        
        GameManager.Instance.events.OnQuotaSet -= UpdateQuotaDisplay;
        GameManager.Instance.events.OnScoreUpdated -= UpdateScoreDisplay;
    }

    private void Start()
    {
        //UpdateQuotaDisplay(0);
        UpdateScoreDisplay(0);
    }

    public void UpdateQuotaDisplay(int newQuota)
    { 
        currentQuota = newQuota;
        score.text = $"Quota: 0 / {currentQuota}";
    }

    public void UpdateScoreDisplay(int newScore)
    {
        score.text = $"Quota: {newScore} / {currentQuota}";
        Debug.Log("Score Updated");
    }
}
