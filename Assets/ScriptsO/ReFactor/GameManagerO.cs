using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerO : MonoBehaviour
{
    public static GameManagerO Instance;
    private bool _gameIsRunning = false;
    public bool GameIsRunning => _gameIsRunning;

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

    public void SetRunningTrue()
    {
        _gameIsRunning = true;
    }

    public void ChechFightStatus()
    {

    }
}
