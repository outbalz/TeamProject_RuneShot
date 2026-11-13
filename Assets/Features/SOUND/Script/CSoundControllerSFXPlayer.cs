using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CSoundControllerSFXPlayer : CSoundController
{
    private void Start()
    {
        AudioSource.outputAudioMixerGroup = CSoundManager.Instance.SFX; 
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlaySound(SoundSO.Clips["Pistol"]);
        }
    }
}
