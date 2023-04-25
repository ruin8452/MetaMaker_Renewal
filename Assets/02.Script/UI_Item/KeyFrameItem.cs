using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyFrameItem : MonoBehaviour, IPointerDownHandler
{
    public Slider slider;
    public GameObject SelectedKey;

    // KeyFrame -> TrackObject
    [HideInInspector] public UnityEvent<int, float> KeyValueChangeEvent;
    [HideInInspector] public UnityEvent<int> SelectedKeyEvent;

    public void OnValueChange_KeyValue(float percent)
    {
        SetKey(percent);
    }

    public void SetKey(float percent)
    {
        slider.value = percent;
        KeyValueChangeEvent?.Invoke(transform.GetSiblingIndex() - 1, slider.value);
    }

    public void OnClick_SelecteKeyFrame()
    {
    }

    public void Deselect()
    {
        SelectedKey.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectedKeyEvent?.Invoke(transform.GetSiblingIndex() - 1);
        SelectedKey.SetActive(true);
    }
}
