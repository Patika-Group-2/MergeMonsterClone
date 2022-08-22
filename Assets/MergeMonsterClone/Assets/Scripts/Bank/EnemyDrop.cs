using UnityEngine;

//this script is atteched to enemy units
public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int _dropAmount;

    //when enemy dies add money to MoneyHolder
    private void OnDestroy()
    {
        BankManager.Instance.CoinDrop.AddCoin(_dropAmount);
    }
}

