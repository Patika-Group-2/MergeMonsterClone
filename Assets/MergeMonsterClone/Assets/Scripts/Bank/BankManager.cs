using UnityEngine;

public class BankManager : MonoBehaviour
{
    public static BankManager Instance;

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
    }

    [SerializeField] public float currentBalance = 1000f;
    
    private void Start() 
    {
        if (PlayerPrefs.HasKey("TotalGold"))
            currentBalance = PlayerPrefs.GetFloat("TotalGold");
    }

    public void Withdraw(float amount)
    {
        currentBalance -= Mathf.Round(amount);
    }

    public void Deposit(float amount)
    {
        currentBalance += Mathf.Round(amount);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalGold", currentBalance);
    }
}
