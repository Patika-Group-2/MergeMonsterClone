using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankManager : MonoBehaviour
{
    [SerializeField] float maxBalance = 1000;
    public float costMoney = 100;
    [SerializeField] public float currentBalance;
    public static int lostCoin;

    //public float CurrentBalance { get { return currentBalance; } }

 
    private void Start() 
    {
        currentBalance = maxBalance;
    }

    public void Withdraw(float amount)
    {
        currentBalance -= Mathf.Round(amount);
    }

    public void Deposit(float amount)
    {
        currentBalance += Mathf.Round(amount);
    }

}
