using UnityEngine;
using UnityEngine.UI;

//Manage all buttons which are belong to this canvas 
public class ConstantCanvasButtonHandler : MonoBehaviour
{
    [SerializeField] private Sprite _muted;
    [SerializeField] private Sprite _notMuted;
    [SerializeField] private Button _muteButton;
    [SerializeField] private AudioClip _buttonClick;
    
    [SerializeField] Slider _soundSlider;

    private void Start()
    {
        _soundSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeVolume(val));
        SoundManager.Instance.ChangeVolume(_soundSlider.value);
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
