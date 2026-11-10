using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CSoundControllerSFX : CSoundController
{
    private void Start()
    {
        AudioSource.outputAudioMixerGroup = CSoundManager.Instance.SFX; 
    }
 
}
