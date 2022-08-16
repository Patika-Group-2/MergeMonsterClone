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

    public void PlaySound(AudioClip clip)
    {
        Source.PlayOneShot(clip);
    }
}
