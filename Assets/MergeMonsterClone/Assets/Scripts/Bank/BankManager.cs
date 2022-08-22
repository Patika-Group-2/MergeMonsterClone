using UnityEngine;

public class BankManager : MonoBehaviour
{
    public static BankManager Instance;
    public CoinDropManager CoinDrop;
    public WinLoseAddMoney WinAddMoney;
    private void Awake()
    {
        if(Instance != null && Instance  != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        CoinDrop = new CoinDropManager();
        WinAddMoney = new WinLoseAddMoney();
    }
    //Starting money amount
    [SerializeField] public float currentBalance = 1000f;
    
    private void Start() 
    {
        if (PlayerPrefs.HasKey("TotalGold"))
            currentBalance = PlayerPrefs.GetFloat("TotalGold");
    }

    //Decrease total money
    public void Withdraw(float amount)
    {
        currentBalance -= Mathf.Round(amount);
    }

    //Increase total money
    public void Deposit(float amount)
    {
        currentBalance += Mathf.Round(amount);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalGold", currentBalance);
    }
}
