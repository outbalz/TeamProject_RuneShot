using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/CSoundSO",fileName = "CSoundSO") ]
public class CSoundSO : ScriptableObject
{
    [SerializeField] CSoundData cSoundData = new CSoundData();

    [NonSerialized] private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> Clips => _clips;

    private void OnEnable()
    {
        _clips.Clear();

        for (int i = 0; i < cSoundData._AudioClipList.Count; i++)
        {
            _clips.Add(cSoundData._AudioClipNames[i], cSoundData._AudioClipList[i]);
        }
        
    }
}
 
[Serializable]
public class CSoundData
{
    public List<AudioClip> _AudioClipList;
    public List<string> _AudioClipNames;
}


