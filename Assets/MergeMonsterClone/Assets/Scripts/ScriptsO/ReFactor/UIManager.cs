using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _winScreenPrefab;
    [SerializeField] private GameObject _loseScreenPrefab;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameManager.Instance.OnWin += LoadWinScreen;
        GameManager.Instance.OnLose += LoadLoseScreen;
    }


    public void LoadWinScreen()
    {
        Instantiate(_winScreenPrefab);
        //Send parameters for text
    }

    public void LoadLoseScreen()
    {
        Instantiate(_loseScreenPrefab);
        //Send parameters for text and images
    }

}
