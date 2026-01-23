using UnityEngine;
using System;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int highScore { get; private set; }

    [Header("Managers")]
    public VolumeManager VolumeManager;

    [Header("Event Container")]
    public Events events;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    void Start()
    {
        events = new Events();

        events.OnScoreUpdated += SetScore;
    }

    private void SetScore(int newScore)
    {
        if (newScore > highScore)
        { 
            newScore = highScore;
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }
}
