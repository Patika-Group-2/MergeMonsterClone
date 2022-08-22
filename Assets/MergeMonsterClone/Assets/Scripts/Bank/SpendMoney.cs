using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Manage shop screen buy buttons
public class SpendMoney : MonoBehaviour
{
    [SerializeField] private float _humanCurrentSpend = 100;
    [SerializeField] private float _dragonCurrentSpend = 100;

    [SerializeField] private Button _humanButton;
    [SerializeField] private Button _dragonButton;

    [SerializeField] private TMP_Text _humanCostText;
    [SerializeField] private TMP_Text _dragonCostText;

    [SerializeField] private AudioClip _buyButtonSound;
    public float DragonCurrentSpend { get => _dragonCurrentSpend; set => _dragonCurrentSpend = value; }
    public float HumanCurrentSpend { get => _humanCurrentSpend; set => _humanCurrentSpend = value; }


    private void Update()
    {
        if (BankManager.Instance.currentBalance < DragonCurrentSpend)
            _dragonButton.enabled = false;
        else
            _dragonButton.enabled = true;

        if (BankManager.Instance.currentBalance < HumanCurrentSpend)
            _humanButton.enabled = false;
        else
            _humanButton.enabled = true;
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HumanCost"))
            HumanCurrentSpend = PlayerPrefs.GetFloat("HumanCost");
        if (PlayerPrefs.HasKey("DragonCost"))
            DragonCurrentSpend = PlayerPrefs.GetFloat("DragonCost");
    }

    private void Start()
    {
        _humanCostText.text = HumanCurrentSpend.ToString();
        _dragonCostText.text = DragonCurrentSpend.ToString();
    }

    public void BuyDragon()
    {
        SoundManager.Instance.PlaySound(_buyButtonSound);
        BankManager.Instance.Withdraw(DragonCurrentSpend);
        CostDragon();
        _dragonCostText.text = DragonCurrentSpend.ToString();
    }

    public void BuyHuman()
    {
        SoundManager.Instance.PlaySound(_buyButtonSound);
        BankManager.Instance.Withdraw(HumanCurrentSpend);
        CostHuman();
        _humanCostText.text = HumanCurrentSpend.ToString();
    }

    public void CostDragon()
    {
        DragonCurrentSpend = Mathf.Round(DragonCurrentSpend + DragonCurrentSpend * 25 / 100);
    }

    public void CostHuman()
    {
        HumanCurrentSpend = Mathf.Round(HumanCurrentSpend + HumanCurrentSpend * 25 / 100);
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("HumanCost", HumanCurrentSpend);
        PlayerPrefs.SetFloat("DragonCost", DragonCurrentSpend);
    }
}
