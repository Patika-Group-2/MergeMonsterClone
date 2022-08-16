using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WinScreenHandler : MonoBehaviour
{
    [SerializeField] private Image[] _starImages;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _totalGold;

    private void Start()
    {
        StarCalculator();
    }
    public void NextButton()
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

    public void SetStars(int starnumber)
    {
        if (starnumber > _starImages.Length)
            return;

        for (int i = 0; i < starnumber; i++)
        {
            _starImages[i].enabled = true;
        }
    }

    public void StarCalculator()
    {
        int beginningPlayer = LevelCreator.Instance.PlayerCountAtBeginning;
        int remainingPlayer = LevelCreator.Instance.PlayerCountAtEnd;
        int stars;

        if (remainingPlayer <= beginningPlayer * 0.3)
            stars = 1;
        else if (remainingPlayer >= beginningPlayer * 0.6)
            stars = 3;
        else
            stars = 2;

        SetStars(stars);
    }
}