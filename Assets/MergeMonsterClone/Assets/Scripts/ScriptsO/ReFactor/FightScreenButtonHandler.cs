using UnityEngine;
using UnityEngine.UI;


public class FightScreenButtonHandler : MonoBehaviour
{
    [SerializeField] private Sprite _muted;
    [SerializeField] private Sprite _notMuted;
    [SerializeField] private Button _muteButton;
    [SerializeField] private AudioClip _buttonClick;

    public void FightButton()
    {
        EntityManager.Instance.SetEntityList();
        GameManager.Instance.SetRunningTrue();
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetPlayerCountAtBegin();
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
}
