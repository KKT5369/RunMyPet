using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class SerializeDictionary : SerializedDictionary<string,AudioClip>{}

public class SoundManager : SingleTon<SoundManager>
{
    [Header("Sound")]
    [SerializeField]
    private SerializeDictionary soundClips = new();


    private GameObject _goBGM;
    private Dictionary<string, GameObject> _effectSounds;
    private AudioSource _bgAudioSource;
    
    GameObject GOBgSound
    {
        get
        {
            if (_goBGM == null)
            {
                _goBGM = new GameObject("BGM");
                _goBGM.AddComponent<AudioSource>();
            }

            return _goBGM;
        }
    }
    
    AudioSource BgAudioSource
    {
        get
        {
            if (_bgAudioSource == null)
            {
                _bgAudioSource = GOBgSound.AddComponent<AudioSource>();
            }

            return _bgAudioSource;
        }
    }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        AudioClip[] _audioClips = Resources.LoadAll<AudioClip>("Audio");
        
        foreach (var v in _audioClips)
        {
            soundClips.Add(v.name,v);
        }
    }

    public void BGMPlay(string clipName,float bgVolume = 1f)
    {
        AudioClip clip;
        if (!soundClips.TryGetValue(clipName, out clip)) return;

        BgAudioSource.clip = clip;
        BgAudioSource.loop = true;
        BgAudioSource.volume = bgVolume;
        BgAudioSource.dopplerLevel = 0;
        BgAudioSource.reverbZoneMix = 0;
        BgAudioSource.Play();
    }

    public void EffectPlay(string clipName)
    {
        GameObject effectgo;
        _effectSounds.TryGetValue(clipName, out effectgo);
        
        AudioClip clip;
        if (!soundClips.TryGetValue(clipName, out clip)) return;
        
        
    }
}
