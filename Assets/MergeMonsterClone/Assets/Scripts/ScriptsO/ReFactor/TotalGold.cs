using TMPro;
using UnityEngine;

// Manage shop screen total gold text 
public class TotalGold : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalGoldAmount;
    void Update()
    {
        _totalGoldAmount.text = BankManager.Instance.currentBalance.ToString();
    }
}
