using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //------------------------------//
    /// <summary>
    /// 单例模式
    /// </summary>
    public static SoundManager Instance = new SoundManager();
    private SoundManager ()
    {

    }
    //------------------------------//


    /// <summary>
    /// 将声音放入字典中，方便管理
    /// </summary>
    private Dictionary<string, AudioClip> _soundDictionary;
    public AudioClip[] musicList;
    private AudioSource [] audioSources;

    private AudioSource bgAudioSource;
    private AudioSource audioSourceEffect;

    void Awake()
    {
        Instance = this;

        _soundDictionary = new Dictionary<string, AudioClip> ();
        musicList = Resources.LoadAll<AudioClip>("Music");
        //本地加载 
        AudioClip [] audioArray = Resources.LoadAll<AudioClip> ("AudioCilp");

        audioSources = GetComponents<AudioSource> ();
        bgAudioSource = audioSources [0];
        audioSourceEffect = audioSources [1];

        //存放到字典
        foreach (AudioClip item in audioArray) 
        {
            _soundDictionary.Add(item.name,item);
        }
    }

    //播放背景音乐
    public void PlayBGaudio(string audioName)
    {
        if (_soundDictionary.ContainsKey(audioName)) 
        {
            bgAudioSource.clip=_soundDictionary[audioName];
            bgAudioSource.Play();
        }
    }
    //播放音效
    public void PlayAudioEffect(string audioEffectName)
    {
        if (_soundDictionary.ContainsKey(audioEffectName)) 
        {
            audioSourceEffect.clip=_soundDictionary[audioEffectName];
            audioSourceEffect.Play();   
        }
    }
}
