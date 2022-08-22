using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    private bool _gameIsRunning = false;
    private int _currentLevel = 1;
    public bool GameIsRunning { get => _gameIsRunning; private set => _gameIsRunning = value; }
    public int CurrentLevel { get => _currentLevel; private set => _currentLevel = value; }

    //event will be triggered win or lose situation 
    public event Action OnWin;
    public event Action OnLose;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    private void Update()
    {
        ChechFightStatus();
    }
    
    public void SetRunningTrue()
    {
        _gameIsRunning = true;
    }

    // Check wheter fight is done or not
    public void ChechFightStatus()
    {
        if (!GameIsRunning)
            return;

        if (LevelCreator.Instance.Enemies.Count == 0)
        {
            // Determine remaining player count to calculate star
            LevelCreator.Instance.SetPlayerCountAtEnd();

            //Instantiate or enable Win Screen
            OnWin?.Invoke();

            // if current level is not max increase current level
            if (CurrentLevel < LevelCreator.Instance.MaxLevel)
                CurrentLevel++;

            GameIsRunning = false;
            //EntityManager.Instance.ClearLists();
            return;
        }

        else if (LevelCreator.Instance.Players.Count == 0)
        {
            //Instantiate or enable Lose Screen
            OnLose?.Invoke();
            GameIsRunning = false;
            //EntityManager.Instance.ClearLists();
            return;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
    }
}
