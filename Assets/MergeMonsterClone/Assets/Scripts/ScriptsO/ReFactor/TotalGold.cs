using TMPro;
using UnityEngine;

public class TotalGold : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalGoldAmount;
    void Update()
    {
        _totalGoldAmount.text = BankManager.Instance.currentBalance.ToString();
    }
}
