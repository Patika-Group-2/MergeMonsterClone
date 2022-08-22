using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource Source;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        Source = GetComponent<AudioSource>();
    }

    //play given clip
    public void PlaySound(AudioClip clip)
    {
        Source.PlayOneShot(clip);
    }

    //stop clip
    public void StopSound()
    {
        Source.Stop();
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
