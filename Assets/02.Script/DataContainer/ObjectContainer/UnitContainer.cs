using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitContainer : MonoBehaviour
{
    public GameObject TrackItemPrefab;

    public string ObjectName;
    public string ObjectClass;
    public Color ObjectColor;
    public bool IsBG;
    public bool IsVisible;
    public bool IsLock;

    public List<KeyFrame> KeyFrames = new List<KeyFrame>();
    [SerializeField] int HandlingKeyNum = 0;

    Vector3 tempPosition = Vector3.zero;
    Vector3 tempRotation = Vector3.zero;
    Vector3 tempScale = Vector3.one;

    TrackObjectItem trackItem;
    ComponentBaseInfo baseInfoCompo;
    ComponentTransform transformCompo;

    Unit unitData;
    Coroutine simulCoroutine;

    private void Awake()
    {
        var ObjectOption = GameObject.FindGameObjectWithTag(Cache.TAG_OBJECT_OPTIONS).GetComponent<ObjectOptionPanel>();
        baseInfoCompo = ObjectOption.BaseInfo;
        transformCompo = ObjectOption.Transform;

        transformCompo.ChangeTanstformPos = SetTanstformPos;
        transformCompo.ChangeTanstformRot = SetTanstformRot;
        transformCompo.ChangeTanstformSca = SetTanstformScale;

        var parent = GameObject.FindGameObjectWithTag(Cache.TAG_OBJECT_LIST).transform;
        trackItem = Instantiate(TrackItemPrefab, parent).GetComponent<TrackObjectItem>();
        trackItem.OnClick_DeleteUnit.AddListener(DeleteUnit);
        trackItem.OnChagedValue_IsVisible.AddListener(SetIsVisible);
        trackItem.OnChagedValue_IsLock.AddListener(SetIsLock);
        trackItem.OnAdd_KeyFrame.AddListener(AddKeyFrame);
        trackItem.OnSelected_KeyFrame.AddListener(SelectedKey);
        trackItem.OnChagedValue_KeyFrame.AddListener(ChangedKeyValue);
    }

    private void Start()
    {
        trackItem.OnClick_AddKeyFrame();
    }

    public void InitContainer(Unit unitContainer)
    {
        unitData = unitContainer;

        switch(unitData.UnitClass)
        {
            case UnitClasses.ModelUnit:
                break;
            case UnitClasses.ImageUnit:
                GetComponentInChildren<Image>().sprite = SpriteStorage.ReadSprite(Path.Combine(PathStorage.ASSETS_FOLDER, unitContainer.UnitName));
                break;
        }
    }

    void DeleteUnit()
    {
        Destroy(gameObject);
    }


    void SetTanstformPos(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempPosition.x = value; break;
            case "Y": tempPosition.y = value; break;
            case "Z": tempPosition.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Position = transform.localPosition = tempPosition;
    }
    void SetTanstformRot(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempRotation.x = value; break;
            case "Y": tempRotation.y = value; break;
            case "Z": tempRotation.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Rotation = transform.localEulerAngles = tempRotation;
    }
    void SetTanstformScale(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempScale.x = value; break;
            case "Y": tempScale.y = value; break;
            case "Z": tempScale.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Scale = transform.localScale = tempScale;
    }

    public void SetUnitActive(bool isActive)
    {
        trackItem.gameObject.SetActive(isActive);
        gameObject.SetActive(isActive);
    }

    public void SetIsVisible(bool isVisible)
    {
        IsVisible = isVisible;
        gameObject.SetActive(isVisible);
    }

    public void SetIsLock(bool isLock)
    {
        IsLock = isLock;
    }

    void AddKeyFrame()
    {
        KeyFrames.Add(new KeyFrame());
    }

    void SelectedKey(int index)
    {
        HandlingKeyNum = index;
        transformCompo.SetTransformComponent(KeyFrames[HandlingKeyNum].Position, KeyFrames[HandlingKeyNum].Rotation, KeyFrames[HandlingKeyNum].Scale);
    }

    void ChangedKeyValue(int index, float percent)
    {
        HandlingKeyNum = index;
        KeyFrames[index].Percent = percent;
    }

    public void PlaySimulateUnit()
    {
        var sortedKeyFrames = KeyFrames.OrderBy(x => x.Percent).ToList();

        var firstKey = (KeyFrame)sortedKeyFrames.First().Clone();
        firstKey.Percent = 0;
        sortedKeyFrames.Insert(0, firstKey);

        simulCoroutine = StartCoroutine(SimulateUnitCoroutine(sortedKeyFrames));
    }

    public void StopSimulateUnit()
    {
        if (simulCoroutine != null)
            StopCoroutine(simulCoroutine);
    }

    IEnumerator SimulateUnitCoroutine(List<KeyFrame> sortedKeyFrames)
    {
        float deltaTime;

        for (int i = 0; i < sortedKeyFrames.Count - 1; i++)
        {
            deltaTime = (sortedKeyFrames[i+1].Percent * TimeLineSetter.RunningTime) - (sortedKeyFrames[i].Percent * TimeLineSetter.RunningTime);
            float currTime = 0;

            while (deltaTime >= currTime)
            {
                currTime += Time.deltaTime / deltaTime;

                transform.localPosition    = Vector3.Lerp(sortedKeyFrames[i].Position, sortedKeyFrames[i+1].Position, currTime);
                transform.localEulerAngles = Vector3.Lerp(sortedKeyFrames[i].Rotation, sortedKeyFrames[i+1].Rotation, currTime);
                transform.localScale       = Vector3.Lerp(sortedKeyFrames[i].Scale,    sortedKeyFrames[i+1].Scale,    currTime);

                //Debug.Log($"{deltaTime} {currTime}");

                yield return null;
            }
        }

        yield break;
    }
}

public class KeyFrame : ICloneable
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public float Percent;

    public KeyFrame()
    {
        Position = Vector3.zero;
        Rotation = Vector3.zero;
        Scale = Vector3.one;
        Percent = 0;
    }

    public object Clone()
    {
        var clone = new KeyFrame
        {
            Position = this.Position,
            Rotation = this.Rotation,
            Scale = this.Scale,
            Percent = this.Percent
        };

        return clone;
    }
}