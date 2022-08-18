using UnityEngine;

public class CoinDropManager : MonoBehaviour
{
    public static CoinDropManager Instance;

    private int loseMoneyHolder;
    public int MoneyHolder { get => loseMoneyHolder; set => loseMoneyHolder = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        MoneyHolder = 0;
    }

    public void AddCoin(int coindrop)
    {
        MoneyHolder =  coindrop;
    }

    public void AddTotalCoin()
    {
        BankManager.Instance.Deposit(MoneyHolder);
        
    }

    public void ResetMoney()
    {
        MoneyHolder = 0;
    }
}
