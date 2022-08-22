using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource _audiosource;
    [SerializeField] private AudioClip _attackSound;

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
        //when OnAttack event triggers play the sound
        GetComponent<Attack>().OnAttack+=AttackSound;
    }

    public void AttackSound()
    {
        _audiosource.PlayOneShot(_attackSound);
    }
}
