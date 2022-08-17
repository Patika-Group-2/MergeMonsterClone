using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropManager : MonoBehaviour
{   
    [SerializeField] int _dropCoin;
    [SerializeField] public int loseMoneyHolder;
    BankManager _bankManager;

   

    public int CurrentDropCoin { get { return _dropCoin;}}
    private void Awake() {
        _bankManager = GetComponent<BankManager>();
        
    }

    public void CoinDrop()
    {
        AddCoin();
        Debug.Log(loseMoneyHolder);
    }
    public void AddCoin()
    {
        loseMoneyHolder = BankManager.lostCoin += _dropCoin;
    }

    public void AddTotalCoin()
    {
        _bankManager.Deposit(loseMoneyHolder);
        Debug.Log(loseMoneyHolder);
        Debug.Log(_bankManager.currentBalance);
    }


}
