using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundControllerBackground : CSoundController
{  
    private void Start()
    {
        AudioSource.outputAudioMixerGroup = CSoundManager.Instance.BackGround; 
    }
     
}
