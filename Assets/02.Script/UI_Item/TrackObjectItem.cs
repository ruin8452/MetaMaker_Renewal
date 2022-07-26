using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrackObjectItem : MonoBehaviour
{
    [Header("키프레임 영역 트랜스폼")]
    public Transform KeyFrameArea;

    [Header("키프레임 프리팹")]
    public GameObject KeyFrameSliderPrefab;

    [Header("기능 토글")]
    public Toggle VisibleToggle;
    public Toggle LockToggle;

    [HideInInspector] public UnityEvent OnClick_DeleteUnit;
    [HideInInspector] public UnityEvent OnAdd_KeyFrame;
    [HideInInspector] public UnityEvent<bool> OnChagedValue_IsVisible;
    [HideInInspector] public UnityEvent<bool> OnChagedValue_IsLock;
    [HideInInspector] public UnityEvent<int> OnSelected_KeyFrame;
    [HideInInspector] public UnityEvent<int, float> OnChagedValue_KeyFrame;

    float runningTime = 1f;
    float currTime = 0f;
    List<KeyFrameItem> keyFrameList = new List<KeyFrameItem>();

    private void Awake()
    {
        GameObject.FindGameObjectWithTag(Cache.TAG_TIMELINE_SETTER).GetComponent<TimeLineSetter>().ChangeTimeLine.AddListener(ListenChangeTimeLine);
    }

    public void OnClick_DeleteAssetObject() => DeleteObject();
    void DeleteObject()
    {
        OnClick_DeleteUnit?.Invoke();
        Destroy(gameObject);
    }

    public void OnClick_ResetKeyFrame()
    {
        keyFrameList.ForEach(x => Destroy(x.gameObject));
        keyFrameList.Clear();
    }

    public void OnToggle_IsVisible(bool isVisible) => OnChagedValue_IsVisible?.Invoke(isVisible);

    public void OnToggle_IsLock(bool isLock) => OnChagedValue_IsLock?.Invoke(isLock);

    public void OnClick_AddKeyFrame() => AddKeyFrame(currTime / runningTime);
    void AddKeyFrame(float keyValue)
    {
        var newKeyFrame = Instantiate(KeyFrameSliderPrefab, KeyFrameArea).GetComponent<KeyFrameItem>();
        newKeyFrame.Index = keyFrameList.Count;
        newKeyFrame.SetKey(keyValue);
        newKeyFrame.OnChange_Key.AddListener(ListenChangedKeyValue);
        newKeyFrame.OnClick_SelectedKey.AddListener(ListenSelectedKeyFrame);

        keyFrameList.Add(newKeyFrame);

        OnAdd_KeyFrame?.Invoke();
    }

    public float[] GetKeyFrames()
    {
        return keyFrameList.Select(x => x.KeyValue).ToArray();

    }

    public void SetKeyFrames(float[] keyValues)
    {
        foreach (var value in keyValues)
            AddKeyFrame(value);
    }

    void ListenChangeTimeLine(float runningTime, float currTime)
    {
        this.runningTime = runningTime;
        this.currTime = currTime;
    }

    void ListenChangedKeyValue(int index, float value)
    {
        var time = value * runningTime;
        Debug.Log($"Index : {index} - {time}");
        OnChagedValue_KeyFrame?.Invoke(index, time);
    }

    void ListenSelectedKeyFrame(int index) => OnSelected_KeyFrame?.Invoke(index);
}
