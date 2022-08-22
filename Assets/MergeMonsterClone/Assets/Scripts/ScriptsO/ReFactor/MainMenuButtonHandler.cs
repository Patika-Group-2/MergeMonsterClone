using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] AudioClip _buttonClickSound;
    private AudioSource _source;

    [SerializeField] PostProcessVolume _volume;

    AutoExposure _autoExposure;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _volume.profile.TryGetSettings(out _autoExposure);
    }
    public void QuitButton()
    {
        _source.PlayOneShot(_buttonClickSound);
        Application.Quit();
    }

    public void PlayButton()
    {
        _source.PlayOneShot(_buttonClickSound);
        StartCoroutine(ShowCor());
    }

    //make transition effect
    IEnumerator ShowCor()
    {
        _autoExposure.keyValue.value = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
