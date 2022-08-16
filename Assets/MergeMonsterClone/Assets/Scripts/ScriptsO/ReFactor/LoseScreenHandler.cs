using UnityEngine;
using TMPro;

public class LoseScreenHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _totalGold;

    public void ReplyButton()
    {
        UIManager.Instance.GoToFightScreen();
        Destroy(gameObject);
    }

    public void ShopButton()
    {
        UIManager.Instance.GoToShopScreen();
        Destroy(gameObject);
    }

    //this func will added after bank system done
    public void SetTexts(int level, int reward, int total)
    {
        _level.text = level.ToString();
        _earnedGold.text = reward.ToString();
        _totalGold.text = total.ToString();
    }
}
