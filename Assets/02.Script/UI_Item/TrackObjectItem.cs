using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrackObjectItem : MonoBehaviour
{
    [Header("키프레임 영역 트랜스폼")]
    public Transform KeyFrameArea;

    [Header("키프레임 프리팹")]
    public GameObject KeyFrameSliderPrefab;

    [Header("이름")]
    public TMP_Text NameText;

    [Header("기능 토글")]
    public Toggle ThisToggle;
    public Toggle VisibleToggle;
    public Toggle LockToggle;

    // TrackObject -> UnitContainer
    [HideInInspector] public UnityEvent SelectedTrackEvent;
    [HideInInspector] public UnityEvent DeleteUnitEvent;
    [HideInInspector] public UnityEvent<bool> IsToggledEvent;
    [HideInInspector] public UnityEvent<bool> IsVisibleChagedEvent;
    [HideInInspector] public UnityEvent<bool> IsLockChagedEvent;

    // TrackObject -> KeyFrame
    [HideInInspector] public UnityEvent DeselectedTrackEvent;

    // KeyFrame -> TrackObject
    [HideInInspector] public UnityEvent<float> AddedKeyFrameEvent;
    [HideInInspector] public UnityEvent<int, float> KeyValueChagedEvent;
    [HideInInspector] public UnityEvent<int> SelectedKeyFrameEvent;

    List<KeyFrameItem> keyFrameList = new List<KeyFrameItem>();

    private void Start()
    {
        OnClick_AddKeyFrame();
    }

    public void OnClick_AddKeyFrame() => AddKeyFrame(TimeLineSetter.CurrTime / TimeLineSetter.RunningTime);
    void AddKeyFrame(float keyValue)
    {
        var newKeyFrame = Instantiate(KeyFrameSliderPrefab, KeyFrameArea).GetComponent<KeyFrameItem>();
        newKeyFrame.SetKey(keyValue);
        newKeyFrame.KeyValueChangeEvent.AddListener(ListenChangedKeyValue);
        newKeyFrame.SelectedKeyEvent.AddListener(ListenSelectedKeyFrame);

        keyFrameList.Add(newKeyFrame);

        AddedKeyFrameEvent?.Invoke(keyValue);

        DeselectedTrackEvent.AddListener(newKeyFrame.Deselect);
    }

    public void OnClick_IsToggled(bool isToggled)
    {
        IsToggledEvent?.Invoke(isToggled);
        if(!isToggled)
            DeselectedTrackEvent?.Invoke();
    }

    public void OnClick_DeleteUnit() => DeleteUnit();
    void DeleteUnit()
    {
        DeleteUnitEvent?.Invoke();
        Destroy(gameObject);
    }

    public void OnClick_ResetKeyFrame()
    {
        keyFrameList.ForEach(x => Destroy(x.gameObject));
        keyFrameList.Clear();

        AddKeyFrame(0);
    }

    public void OnDeselect_TrackObject()
    {
        DeselectedTrackEvent?.Invoke();
    }


    public void OnToggle_IsVisible(bool isVisible) => IsVisibleChagedEvent?.Invoke(isVisible);

    public void OnToggle_IsLock(bool isLock) => IsLockChagedEvent?.Invoke(isLock);


    public void ListenChangeName(string name) => NameText.text = name;

    void ListenChangedKeyValue(int index, float value) => KeyValueChagedEvent?.Invoke(index, value);

    void ListenSelectedKeyFrame(int index)
    {
        SelectedKeyFrameEvent?.Invoke(index);
        keyFrameList.Where(x => x != keyFrameList[index]).ToList().ForEach(x => x.Deselect());
    }
}