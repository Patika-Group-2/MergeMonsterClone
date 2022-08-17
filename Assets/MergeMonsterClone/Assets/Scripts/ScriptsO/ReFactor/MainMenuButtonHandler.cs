using UnityEngine;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] AudioClip _buttonClickSound;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    public void QuitButton()
    {
        _source.PlayOneShot(_buttonClickSound);
        Application.Quit();
    }

    public void PlayButton()
    {
        _source.PlayOneShot(_buttonClickSound);
        //Load Scene
    }
}
