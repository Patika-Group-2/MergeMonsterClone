public class WinLoseAddMoney 
{
    public float _winPrize;
    
    public void AddMoney()
    {
        _winPrize = LevelCreator.Instance.CurrentLevel._coinWins;
        BankManager.Instance.Deposit(_winPrize);
    }
}
