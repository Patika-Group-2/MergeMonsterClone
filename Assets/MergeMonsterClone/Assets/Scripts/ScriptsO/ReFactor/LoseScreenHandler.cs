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
        LevelCreator.Instance.SetLevel();
        Destroy(gameObject);
        //After destroy make transition effect
    }

    public void ShopButton()
    {
        //
    }

    //this func will added after bank system done
    public void SetTexts(int level, int reward, int total)
    {
        _level.text = level.ToString();
        _earnedGold.text = reward.ToString();
        _totalGold.text = total.ToString();
    }
}
