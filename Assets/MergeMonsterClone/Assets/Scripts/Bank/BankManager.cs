using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankManager : MonoBehaviour
{
    [SerializeField] float maxBalance = 1000;
    public float costMoney = 100;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float currentBalance;
    public float CurrentBalance { get { return currentBalance; } }

    private void Awake() 
    {
        _text = GetComponent<TextMeshProUGUI>();
        currentBalance = maxBalance;
        _text.text = costMoney.ToString();
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
