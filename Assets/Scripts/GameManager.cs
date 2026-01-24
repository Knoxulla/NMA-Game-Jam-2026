using UnityEngine;
using System;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
  
    public int currentScore { get; private set; }

    [Header("Managers")]
    public VolumeManager VolumeManager;

    [Header("Event Container")]
    public Events events;

    private void OnEnable()
    {
        events.OnScoreUpdated += SetScore;

    }

    private void OnDisable()
    {
        events.OnScoreUpdated -= SetScore;

    }

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

        currentScore = 0;

    }

    private void SetScore(int newScore)
    {
        currentScore = newScore;
    }
}
