using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageUnit : UnitContainer, IUnit
{
    [Header("Æ®·¢ ÇÁ¸®ÆÕ")]
    public GameObject TrackItemPrefab;

    [Header("ÄÄÆ÷³ÍÆ® ÇÁ¸®ÆÕ")]
    public GameObject BaseInfoPrefab;
    public GameObject TransformCompoPrefab;

    [Header("À¯´Ö ÀÌ¹ÌÁö ÄÄÆ÷³ÍÆ®")]
    public Image UnitImage;

    StepContainer parentStep;

    List<IComponent> components = new List<IComponent>();
    ComponentBaseInfo baseInfoCompo;
    ComponentTransform transformCompo;

    Vector3 tempPosition = Vector3.zero;
    Vector3 tempRotation = Vector3.zero;
    Vector3 tempScale = Vector3.one;

    [SerializeField] int HandlingKeyNum = 0;
    Coroutine simulCoroutine;

    public UnityEvent<string> EditNameEvent;

    private void Awake()
    {
        var timeLine = GameObject.FindGameObjectWithTag(Cache.TAG_TIMELINE_SETTER).GetComponent<TimeLineSetter>();
        //timeLine.CurrTimeChangeEvent.AddListener(UnitPreview);

         parentStep = transform.parent.GetComponent<StepContainer>();

        var optionTransform = GameObject.FindGameObjectWithTag(Cache.TAG_OBJECT_OPTIONS).transform;
        baseInfoCompo = Instantiate(BaseInfoPrefab, optionTransform).GetComponent<ComponentBaseInfo>();
        baseInfoCompo.EditNameEvent.AddListener(SetUnitName);

        transformCompo = Instantiate(TransformCompoPrefab, optionTransform).GetComponent<ComponentTransform>();

        transformCompo.ChangeTanstformPos.AddListener(ListenSetTanstformPos);
        transformCompo.ChangeTanstformRot.AddListener(ListenSetTanstformRot);
        transformCompo.ChangeTanstformSca.AddListener(ListenSetTanstformScale);

        components.Add(baseInfoCompo);
        components.Add(transformCompo);


        var parent = GameObject.FindGameObjectWithTag(Cache.TAG_OBJECT_LIST).transform;
        trackItem = Instantiate(TrackItemPrefab, parent).GetComponent<TrackObjectItem>();
        trackItem.ThisToggle.group = parentStep.GetComponent<ToggleGroup>();
        EditNameEvent.AddListener(trackItem.ListenChangeName);

        trackItem.IsVisibleChagedEvent.AddListener(SetIsVisible);
        trackItem.IsLockChagedEvent.AddListener(SetIsLock);
        trackItem.IsToggledEvent.AddListener(IsSelectedUnit);
        trackItem.AddedKeyFrameEvent.AddListener(AddKeyFrame);
        trackItem.SelectedKeyFrameEvent.AddListener(SelectedKey);
        trackItem.KeyValueChagedEvent.AddListener(ChangedKeyValue);
        trackItem.DeleteUnitEvent.AddListener(DeleteUnit);
    }

    private void Start()
    {
        baseInfoCompo.Picker.onValueChanged.AddListener(color => UnitImage.color = color);
    }

    public void InitContainer(Unit unitContainer)
    {
        unitData = unitContainer;

        switch (unitData.UnitClass)
        {
            case UnitClasses.ModelUnit:
                break;
            case UnitClasses.ImageUnit:
                UnitImage.sprite = SpriteStorage.ReadSprite(Path.Combine(PathStorage.ASSETS_FOLDER, unitData.UnitName));
                break;
        }
    }

    public void SetUnitActive(bool isActive)
    {
        trackItem.gameObject.SetActive(isActive);
        gameObject.SetActive(isActive);
        components.ForEach(x => x.SetActive(false));
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

    public void IsSelectedUnit(bool isSelected)
    {
        components.ForEach(x => x.SetActive(isSelected));
    }

    void DeleteUnit()
    {
        Destroy(gameObject);
    }

    void SetUnitName(string name)
    {
        UnitName = name;
        EditNameEvent?.Invoke(name);
    }

    void ListenSetTanstformPos(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempPosition.x = value; break;
            case "Y": tempPosition.y = value; break;
            case "Z": tempPosition.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Position = transform.localPosition = tempPosition;
    }
    void ListenSetTanstformRot(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempRotation.x = value; break;
            case "Y": tempRotation.y = value; break;
            case "Z": tempRotation.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Rotation = transform.localEulerAngles = tempRotation;
    }
    void ListenSetTanstformScale(string xyz, float value)
    {
        switch (xyz)
        {
            case "X": tempScale.x = value; break;
            case "Y": tempScale.y = value; break;
            case "Z": tempScale.z = value; break;
        }
        KeyFrames[HandlingKeyNum].Scale = transform.localScale = tempScale;
    }

    void UnitPreview(float percentTime)
    {
        int index = 0;
        var sortedKeyFrames = KeyFrames.OrderBy(x => x.Percent).ToList();

        if (percentTime <= sortedKeyFrames.First().Percent)
        {
            transform.localPosition = sortedKeyFrames.First().Position;
            transform.localEulerAngles = sortedKeyFrames.First().Rotation;
            transform.localScale = sortedKeyFrames.First().Scale;

            transformCompo.SetTransformComponent(transform.localPosition, transform.localEulerAngles, transform.localScale);
            return;
        }
        else if(percentTime >= sortedKeyFrames.Last().Percent)
        {
            transform.localPosition = sortedKeyFrames.Last().Position;
            transform.localEulerAngles = sortedKeyFrames.Last().Rotation;
            transform.localScale = sortedKeyFrames.Last().Scale;

            transformCompo.SetTransformComponent(transform.localPosition, transform.localEulerAngles, transform.localScale);
            return;
        }


        for (index = 0; index < sortedKeyFrames.Count()-1; index++)
        {
            if (sortedKeyFrames[index].Percent <= percentTime && percentTime <= sortedKeyFrames[index+1].Percent)
            {
                float delta = sortedKeyFrames[index + 1].Percent - sortedKeyFrames[index].Percent;
                float lerp = (percentTime - sortedKeyFrames[index].Percent) / delta;

                transform.localPosition = Vector3.Lerp(sortedKeyFrames[index].Position, sortedKeyFrames[index + 1].Position, lerp);
                transform.localEulerAngles = Vector3.Lerp(sortedKeyFrames[index].Rotation, sortedKeyFrames[index + 1].Rotation, lerp);
                transform.localScale = Vector3.Lerp(sortedKeyFrames[index].Scale, sortedKeyFrames[index + 1].Scale, lerp);

                transformCompo.SetTransformComponent(transform.localPosition, transform.localEulerAngles, transform.localScale);
                return;
            }
        }
    }


    // Key Frame ///////////////////////////////
    void AddKeyFrame(float percent)
    {
        KeyFrames.Add(new KeyFrame(percent));
        HandlingKeyNum = KeyFrames.Count - 1;
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



    // Simulation ///////////////////////////////
    public void PlaySimulateUnit(bool doLoop)
    {
        var sortedKeyFrames = KeyFrames.OrderBy(x => x.Percent).ToList();

        simulCoroutine = StartCoroutine(SimulateUnitCoroutine(sortedKeyFrames, doLoop));
    }

    public void StopSimulateUnit()
    {
        if (simulCoroutine != null)
            StopCoroutine(simulCoroutine);
    }

    IEnumerator SimulateUnitCoroutine(List<KeyFrame> sortedKeyFrames, bool doLoop)
    {
        float deltaTime;
        float currMovingTime = 0f;
        var timeRatio = sortedKeyFrames.Select(x => x.Percent * TimeLineSetter.RunningTime).ToList();

        do
        {
            transform.localPosition = sortedKeyFrames[0].Position;
            transform.localEulerAngles = sortedKeyFrames[0].Rotation;
            transform.localScale = sortedKeyFrames[0].Scale;

            yield return Cache.CachingWaitForSecond(timeRatio[0]);

            for (int i = 0; i < sortedKeyFrames.Count - 1; i++)
            {
                deltaTime = timeRatio[i+1] - timeRatio[i];
                float lerpSpeed = 0f;
                currMovingTime = 0f;

                while (deltaTime >= currMovingTime)
                {
                    lerpSpeed += Time.deltaTime / deltaTime;
                    currMovingTime += Time.deltaTime;

                    transform.localPosition    = Vector3.Lerp(sortedKeyFrames[i].Position, sortedKeyFrames[i+1].Position, lerpSpeed);
                    transform.localEulerAngles = Vector3.Lerp(sortedKeyFrames[i].Rotation, sortedKeyFrames[i+1].Rotation, lerpSpeed);
                    transform.localScale       = Vector3.Lerp(sortedKeyFrames[i].Scale,    sortedKeyFrames[i+1].Scale,    lerpSpeed);

                    yield return null;
                }
            } 

            yield return Cache.CachingWaitForSecond(TimeLineSetter.RunningTime - timeRatio.Last());

        } while (doLoop);

        yield break;
    }
}
