using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseAddMoney : MonoBehaviour
{
    [SerializeField] List<EnemyDataSO> _levels;
    [SerializeField] BankManager _bankManager;
    private EnemyDataSO _currentLevel;
    private int _maxLevel;
    [SerializeField] public float _winWin;
    
    CoinDropManager _coinDrop;
    public int LevelMax { get => _maxLevel; private set => _maxLevel = value; }
    //public float WinWin { get { return _winWin; } }
   


    private void Start() {
        
        LevelMax = _levels.Count;
        _currentLevel = _levels[(GameManager.Instance.CurrentLevel-1)];
        _winWin = _currentLevel._coinWins;
        

    }
    public void AddMoney()
    {
        _currentLevel = _levels[GameManager.Instance.CurrentLevel - 1];
        _winWin = _currentLevel._coinWins;
        LevelMax = _levels.Count;
        _bankManager.Deposit(_winWin);
        

        Debug.Log(_winWin);

    }


}
