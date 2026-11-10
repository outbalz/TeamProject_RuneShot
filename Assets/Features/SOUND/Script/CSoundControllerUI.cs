using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundControllerUI : CSoundController
{ 
    private void Start()
    {
        AudioSource.outputAudioMixerGroup = CSoundManager.Instance.UI;
    }
      
}
