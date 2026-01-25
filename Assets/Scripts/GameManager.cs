using UnityEngine;
using System;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isBadEnd = true;
    public int currentScore { get; private set; }

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

        events = new Events();

        SetScore(0);

    }

    public void SetScore(int newScore)
    {
        currentScore = newScore;
    }
}
