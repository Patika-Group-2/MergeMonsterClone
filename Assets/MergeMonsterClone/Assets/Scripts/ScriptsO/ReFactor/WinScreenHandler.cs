using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Manage win screen
public class WinScreenHandler : MonoBehaviour
{
    [SerializeField] private Image[] _starImages;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _totalGold;
    [SerializeField] private AudioClip _winSound;
    
    private void Start()
    {
        StarCalculator();
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlaySound(_winSound);
    }
    public void NextButton()
    {
        UIManager.Instance.GoToFightDirecetly();
        Destroy(gameObject);
    }

    public void ShopButton()
    {
        UIManager.Instance.GoToShopDirecetly();
        Destroy(gameObject);
    }

    //Set texts
    public void SetTexts(int level, float reward, float total)
    {
        _level.text = level.ToString();
        _earnedGold.text = reward.ToString();
        _totalGold.text = total.ToString();
    }

    //Set star count
    public void SetStars(int starnumber)
    {
        if (starnumber > _starImages.Length)
            return;

        for (int i = 0; i < starnumber; i++)
        {
            _starImages[i].enabled = true;
        }
    }

    //Calculate how many stars gained
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
