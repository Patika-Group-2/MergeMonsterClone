using UnityEngine;

public class WinLoseAddMoney : MonoBehaviour
{
    public static WinLoseAddMoney Instance;

    public float _winWin;

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
    }
    public void AddMoney()
    {
        _winWin = LevelCreator.Instance.CurrentLevel._coinWins;
        BankManager.Instance.Deposit(_winWin);
    }
}
