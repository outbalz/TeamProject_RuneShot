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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            PlaySound(SoundSO.Clips["Inventory"]);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            PlaySound(SoundSO.Clips["Craft"]);
        }
        
    }
}
