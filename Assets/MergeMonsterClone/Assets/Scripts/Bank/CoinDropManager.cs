using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropManager : MonoBehaviour
{   
    [SerializeField] int _dropCoin;
    
    [SerializeField] public int loseMoneyHolder = 500;
    BankManager _bankManager;
    

    private void Awake() {
        _bankManager = GetComponent<BankManager>();
        
        
    }
    public void AddCoin()
    {
        loseMoneyHolder = BankManager.lostCoin += _dropCoin;
        Debug.Log(loseMoneyHolder);
    }

    public void AddTotalCoin()
    {
        _bankManager.Deposit(loseMoneyHolder);
        Debug.Log(loseMoneyHolder);
        Debug.Log(_bankManager.currentBalance);
    }


}
