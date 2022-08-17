using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpendMoney : MonoBehaviour
{
    [SerializeField] float firstSpend = 100;
    [SerializeField] float currentSpend;
    [SerializeField] Button _btn;
    public float CurrentSpend { get { return currentSpend; } }

    BankManager bankManager;


    private void Awake()
    {
        bankManager = GetComponent<BankManager>();
        _btn = GetComponent<Button>();
        currentSpend = firstSpend;
    }

    public void BuyCharacter()
    {
        if (bankManager.currentBalance < currentSpend)
        {
            _btn.enabled = false;
        }
        else
        {
            _btn.enabled = true;
            bankManager.Withdraw(currentSpend);
            CostMoney();
        }
    }

    void CostMoney()
    {
        currentSpend = Mathf.Round(currentSpend + currentSpend * 25 / 100);
    }
}
