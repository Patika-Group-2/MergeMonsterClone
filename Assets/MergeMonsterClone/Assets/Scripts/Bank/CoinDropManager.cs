using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropManager : MonoBehaviour
{
    [SerializeField] int _dropCoin;
    BankManager _bankManager;

    int loseMoneyHolder;

    public int CurrentDropCoin { get { return _dropCoin; } }


    private void Awake()
    {
        _bankManager = GetComponent<BankManager>();
    }

    public void CoinDrop()
    {
        AddCoin();
    }

    public void AddCoin()
    {
        loseMoneyHolder = BankManager.lostCoin += _dropCoin;
    }

    public void AddTotalCoin()
    {
        _bankManager.Deposit(loseMoneyHolder);
        loseMoneyHolder = 0;
    }
}
