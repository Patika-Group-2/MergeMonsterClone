using UnityEngine;
using UnityEngine.UI;


public class FightScreenButtonHandler : MonoBehaviour
{
    [SerializeField] private Sprite _muted;
    [SerializeField] private Sprite _notMuted;
    [SerializeField] private Button _muteButton;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _ambientSound;
    [SerializeField] Slider _soundSlider;


    private void Start()
    {
        _soundSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeVolume(val));
        SoundManager.Instance.ChangeVolume(_soundSlider.value);
    }
    
    public void FightButton()
    {
        EntityManager.Instance.SetEntityList();
        GameManager.Instance.SetRunningTrue();
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetPlayerCountAtBegin();
        SoundManager.Instance.PlaySound(_ambientSound);
        UIManager.Instance.ResetCanvas();
    }

    public void MuteButton()
    {
        SoundManager.Instance.PlaySound(_buttonClick);
        AudioListener.pause = !AudioListener.pause;

        if (AudioListener.pause)
            _muteButton.GetComponent<Image>().sprite = _muted;
        else
            _muteButton.GetComponent<Image>().sprite = _notMuted;
    }

    public void SoundButton()
    {
        if (_soundSlider.gameObject.activeSelf)
            _soundSlider.gameObject.SetActive(false);
        else
            _soundSlider.gameObject.SetActive(true);
        
    }
}
