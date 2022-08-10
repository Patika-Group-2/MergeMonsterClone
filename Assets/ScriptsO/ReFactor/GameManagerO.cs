using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerO : MonoBehaviour
{
    public static GameManagerO Instance;
    public bool GameIsRunning = false;

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
        GameIsRunning = true;
    }

    public void ChechFightStatus()
    {

    }
}
