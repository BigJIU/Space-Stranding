using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public enum SliderState
{
    Normal, Down, Up
}
public class SliderUI :Slider{
    public static SliderState State = SliderState.Normal;//默认状态2，按下时候1，松开时候0
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        State = SliderState.Down;
        print("按下");
 
    }
    public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        State = SliderState.Up;
        print("松开");
    }
    public void Reset() {
        State = SliderState.Normal;
    }
 
}