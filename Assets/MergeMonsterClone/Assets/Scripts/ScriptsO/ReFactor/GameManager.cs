using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _gameIsRunning = false;
    private int _currentLevel = 1;
    public bool GameIsRunning { get => _gameIsRunning; private set => _gameIsRunning = value; }
    public int CurrentLevel { get => _currentLevel; private set => _currentLevel = value; }
    
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
    }

    private void Update()
    {
        ChechFightStatus();
    }

    public void SetRunningTrue()
    {
        _gameIsRunning = true;
    }

    public void ChechFightStatus()
    {
        if (!GameIsRunning)
            return;

        if (EntityManager.Instance.Enemies.Count == 0)
        {
            //Instantiate or enable Win Screen

            if(CurrentLevel < LevelCreator.Instance.MaxLevel)
            CurrentLevel++;

            GameIsRunning = false;
            EntityManager.Instance.ClearLists();
            return;
        }

        else if (EntityManager.Instance.Players.Count == 0)
        {
            //Instantiate or enable Lose Screen
            GameIsRunning = false;
            EntityManager.Instance.ClearLists();
            return;
        }
    }
}
