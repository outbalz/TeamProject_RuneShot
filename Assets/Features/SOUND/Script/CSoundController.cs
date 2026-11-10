using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private CSoundSO _soundSO;

    private Action _soundAction;
    public CSoundSO SoundSO => _soundSO;
    public AudioSource AudioSource { get { return _audioSource; } set { _audioSource = value; } } 
    
    protected virtual void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        } 
    }

    public void RegisterSound(AudioClip clip)
    {
        _soundAction = null;

        _soundAction = () => AudioSource.PlayOneShot(clip);
    }

    public void PlaySound()
    {
        _soundAction?.Invoke();
    }
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    public void PlayOtherBackGround(AudioClip clip)
    {
        AudioSource.clip = clip;

        AudioSource.Play();
    }
}
