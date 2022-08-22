using UnityEngine;
using TMPro;

//Manage lose screen 
public class LoseScreenHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _totalGold;
    [SerializeField] private AudioClip _loseSound;

    private void Start()
    {
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlaySound(_loseSound);
    }
    public void ReplyButton()
    {
        UIManager.Instance.GoToFightDirecetly();
        Destroy(gameObject);
    }

    public void ShopButton()
    {
        UIManager.Instance.GoToShopDirecetly();
        Destroy(gameObject);
    }

    //Set lose screen texts
    public void SetTexts(int level, float reward, float total)
    {
        _level.text = level.ToString();
        _earnedGold.text = reward.ToString();
        _totalGold.text = total.ToString();
    }
}
