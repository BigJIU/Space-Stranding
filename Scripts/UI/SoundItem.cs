using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//挂载每个音乐信息显示UI
public class SoundItem : MonoBehaviour {
 
    //歌曲委托，index歌曲索引
    public event System.Action<int> SOUNDPLAY;
    //保存索引
    private int index=-1;
    private Text nameUI;
    private Text timerUI;
    private void Awake()
    {
        nameUI = transform.Find("name").GetComponent<Text>();
        timerUI = transform.Find("time").GetComponent<Text>();
    }
    void Start () {
        Button but = transform.Find("Button").GetComponent<Button>();
        but.onClick.AddListener(OnClick);
    }
    public void InitItemInfo(string soundName,float seconds,int index)
    {
 
        nameUI.text = soundName;
        timerUI.text = getTime(seconds);
        this.index = index;
    }
    //时间格式
    private string getTime(float len) {
        int f =(int) len/60;
        int h = f / 60;
        int s = (int)len % 60;
        return /*(h < 10 ? "0" + h : h.ToString()) + ":" +*/ (f < 10 ? "0" + f : f.ToString()) + ":" + (s<10?"0"+s:s.ToString());
    }
    /// <summary>
    /// 点击歌曲应该播放这首歌，触发歌曲播放事件
    /// </summary>
    private void OnClick() {
        if(SOUNDPLAY!=null&&index!=-1)
            SOUNDPLAY(index);
    }
    public void AddListener(System.Action<int> method) {
        SOUNDPLAY += method;
    }
}