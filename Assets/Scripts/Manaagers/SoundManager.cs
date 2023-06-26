using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
    private List<GameObject> _effectSounds;
    private GameObject _effectSoundBox;
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
        _effectSoundBox = new GameObject("EffectSoundBox");
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
        Transform clipBoxTransform = _effectSoundBox.transform.Find(clipName);
        AudioClip audioClip;
        if (!soundClips.TryGetValue(clipName,out audioClip)) return;
        GameObject clipSoundBox;
        AudioSource audioSource;
        
        if (clipBoxTransform != null)
        {
            for (int i = 0; i < clipBoxTransform.childCount; i++)
            {
                var sound = clipBoxTransform.GetChild(i).gameObject;
                if(!sound.activeInHierarchy)
                {
                    sound.SetActive(true);
                    StartCoroutine(EffectSoundClose(sound.GetComponent<AudioSource>()));
                    return;
                }
            }
            var goSound = new GameObject(clipName);
            audioSource = goSound.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
            goSound.transform.parent = clipBoxTransform.transform;
            StartCoroutine(EffectSoundClose(audioSource));
        }
        else
        {
            clipSoundBox = new GameObject(clipName);
            clipSoundBox.name = clipName;
            clipSoundBox.transform.parent = _effectSoundBox.transform;

            var goSound = new GameObject(clipName);
            audioSource = goSound.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
            goSound.transform.parent = clipSoundBox.transform;
            StartCoroutine(EffectSoundClose(audioSource));
        }
    }

    IEnumerator EffectSoundClose(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audioSource.gameObject.SetActive(false);
    }

}
