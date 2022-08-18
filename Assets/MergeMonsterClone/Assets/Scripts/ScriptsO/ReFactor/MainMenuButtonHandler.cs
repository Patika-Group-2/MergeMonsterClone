using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
