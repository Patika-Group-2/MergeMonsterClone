using UnityEngine;
using TMPro;

public class LoseScreenHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _totalGold;
    [SerializeField] private AudioClip _loseSound;

    private void Start()
    {
        SoundManager.Instance.PlaySound(_loseSound);
    }
    public void ReplyButton()
    {
        UIManager.Instance.GoToFightDirecetly();
        Destroy(gameObject);
        //After destroy make transition effect
    }

    public void ShopButton()
    {
        UIManager.Instance.GoToShopDirecetly();
        Destroy(gameObject);
    }

    //this func will added after bank system done
    public void SetTexts(int level, float reward, float total)
    {
        _level.text = level.ToString();
        _earnedGold.text = reward.ToString();
        _totalGold.text = total.ToString();
    }
}
