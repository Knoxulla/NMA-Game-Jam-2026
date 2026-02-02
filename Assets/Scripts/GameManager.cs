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

    [Header("Instanced Menus")]
    [SerializeField] GameObject optMenu;

    [Header("Event Container")]
    public Events events;

    [SerializeField] bool isPaused = false;

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

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void SetScore(int newScore)
    {
        currentScore = newScore;
    }

    public void OptionMenu()
    {
        if (!isPaused)
        {
            OpenOptions();
        }
        else 
        {
            CloseOptions();
        }

        isPaused = !isPaused;
    }

    private void OpenOptions()
    {
        events.PauseGame();
        Instantiate(optMenu);

    }

    private void CloseOptions()
    {
        events.PauseGame();
    }
}
