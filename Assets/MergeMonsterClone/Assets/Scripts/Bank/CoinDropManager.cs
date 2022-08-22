public class CoinDropManager 
{
    private int _loseMoneyHolder = 0;
    public int MoneyHolder { get => _loseMoneyHolder; set => _loseMoneyHolder = value; }

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
