using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class CSoundManager : CSingletonDontDstroyAtLoad<CSoundManager>
{ 
    [SerializeField] private AudioMixerGroup _master;
    [SerializeField] private AudioMixerGroup _backGround;
    [SerializeField] private AudioMixerGroup _sfx;
    [SerializeField] private AudioMixerGroup _ui;
   
    public AudioMixerGroup Master => _master;
    public AudioMixerGroup BackGround => _backGround;
    public AudioMixerGroup SFX => _sfx; 
    public AudioMixerGroup UI => _ui; 
}
