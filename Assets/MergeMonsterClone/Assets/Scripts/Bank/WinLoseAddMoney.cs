using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseAddMoney : MonoBehaviour
{
    [SerializeField] List<EnemyDataSO> _levels;
    private EnemyDataSO _currentLevel;
    private int _maxLevel;
    private int _winWin;
    public int LevelMax { get => _maxLevel; private set => _maxLevel = value; }
   


    private void Start() {
        LevelMax = _levels.Count;
        _currentLevel = _levels[(GameManager.Instance.CurrentLevel-1)];
        _winWin = _currentLevel._coinWins;
    }
    public void AddMoney()
    {
        _currentLevel = _levels[GameManager.Instance.CurrentLevel - 1];
        _winWin = _currentLevel._coinWins;
        Debug.Log(_winWin);
        Debug.Log(_currentLevel);
        Debug.Log(LevelMax);
    }
}