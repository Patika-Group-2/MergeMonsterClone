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

    [SerializeField] WinLoseAddMoney _wLan;

    [SerializeField] CoinDropManager _cDM;

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
            _wLan.AddMoney();
            LevelCreator.Instance.SetPlayerCountAtEnd();
            //Instantiate or enable Win Screen
            OnWin?.Invoke();
            if(CurrentLevel < LevelCreator.Instance.MaxLevel)
            CurrentLevel++;
            GameIsRunning = false;
            EntityManager.Instance.ClearLists();
            return;
        }

        else if (EntityManager.Instance.Players.Count == 0)
        {
            _cDM.AddTotalCoin();
            //Instantiate or enable Lose Screen
            OnLose?.Invoke();
            GameIsRunning = false;
            EntityManager.Instance.ClearLists();
            return;
        }
    }
}
