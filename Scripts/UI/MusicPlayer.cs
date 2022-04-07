using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    AudioSource source;
    RectTransform content;
    SliderUI slider;//进度控制
    Slider volume;//音量控制
 
    int currentIndex = 0;//当前音频索引
                         // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();//获该脚本下的音频源组件
        //content = GameObject.Find("Content").GetComponent<RectTransform>();//找到Scroll View 下的Content 用来存放预设体
        slider = GameObject.Find("Slider").GetComponent<SliderUI>();//获取进度条下的自定义SliderUI:Slider组件
        //volume = GameObject.Find("Volume").GetComponent<Slider>();//获取音量进度条
        InitSoundList();//初始化播放列表
    }
 
    // Update is called once per frame
    void Update()
    {
        
        if (source == null || source.clip == null) return;
        if (source.isPlaying && SliderUI.State == SliderState.Normal)
        {
 
            print("当前歌曲下标：" + currentIndex);
            slider.value = source.time / source.clip.length;//进度条实时更新
            //判断歌曲是否播放完毕，开始播放下一首
            if (source.time>=source.clip.length*0.99f) {
                OnPlayNext();
            }
            //source.volume = volume.value;//音量实时更新
        }
        //拖拽滑块播放
        if (SliderUI.State == SliderState.Up)
        {
            source.time = slider.value * source.clip.length;
            slider.Reset();
        }
    }
    void InitSoundList()
    {
        //根据加载的歌曲资源将其添加到播放列表中
        for (var i = 0; i < SoundManager.Instance.musicList.Length; i++)
        {
            AudioClip clip = SoundManager.Instance.musicList[i];
            GameObject Amusic = GameObject.Find("Player");
 
 
            //动态添加脚本
            SoundItem item = Amusic.AddComponent<SoundItem>();
            item.InitItemInfo(clip.name, clip.length, i);
            item.AddListener(ToPlay);
        }
    }
 
    //点击播放
    private void ToPlay(int obj)
    {
        source.time = 0;
        if (source.isPlaying)
        {
            source.Stop();
        }
        currentIndex = obj;
        source.clip = SoundManager.Instance.musicList[obj];
        source.Play();
    }
 
    //播放暂停按钮
    public void OnPlayPause()
    {
 
        if (source.isPlaying)
        {
            source.Pause();
            return;
        }
        if (source.clip == null)
        {
            source.clip = SoundManager.Instance.musicList[currentIndex];
        }
        source.Play();
    }
    //播放下一曲
    public void OnPlayNext()
    {
        source.time = 0;
        currentIndex++;
        if (currentIndex >= SoundManager.Instance.musicList.Length) 
        {
            currentIndex = 0;
 
        }
        source.clip = SoundManager.Instance.musicList[currentIndex];
        source.Play();
 
    }
    //播放上一曲
    public void OnPlayLast()
    {
        source.time = 0;
        currentIndex--;
        if (currentIndex<0) {
            currentIndex = SoundManager.Instance.musicList.Length - 1;
        }
        source.clip = SoundManager.Instance.musicList[currentIndex];
        source.Play();
      
    }
}
