using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyFrameItem : MonoBehaviour
{
    public int Index;
    public Slider slider;
    float keyValue;
    public float KeyValue
    {
        get { return keyValue; }
        set { slider.value = keyValue = value; }
    }

    [HideInInspector] public UnityEvent<int, float> OnChange_Key;
    [HideInInspector] public UnityEvent<int> OnClick_SelectedKey;

    public void SetKey(float percent)
    {
        KeyValue = percent;
        OnChange_Key?.Invoke(Index, KeyValue);
    }

    public void OnValueChange_MoveKeyFrame(float percent)
    { 
        keyValue = percent;
        OnChange_Key?.Invoke(Index, KeyValue);
    }

    public void OnClick_SelecteKeyFrame()
    {
        OnClick_SelectedKey?.Invoke(Index);
    }
}
