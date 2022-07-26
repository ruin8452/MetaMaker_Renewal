using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StepContainer : MonoBehaviour
{
    public GameObject StepIndexBoxPrefab;

    public int StepNum;
    public bool DoLoop;
    public int HandlingUnitNum;

    [HideInInspector] public UnityEvent<int> OnSend_SeletedStepNumber;
    [HideInInspector] public UnityEvent<int, int> OnSend_ChangeStepOrder;

    Transform StepsAreaTransform;
    StepIndexBox stepIndexBox;

    List<UnitContainer> unitObjects = new List<UnitContainer>();

    private void Awake()
    {
        StepsAreaTransform = GameObject.FindGameObjectWithTag(Cache.TAG_STEPS_AREA).transform;

        stepIndexBox = Instantiate(StepIndexBoxPrefab, StepsAreaTransform).GetComponent<StepIndexBox>();
        stepIndexBox.SelectedStep = ListenSelectedStep;
        stepIndexBox.ChangeOrder = ListenChangeOrder;
    }

    public void SetStepNum(int stepNum)
    {
        StepNum = stepNum;
        stepIndexBox.SetIndex(stepNum);
    }

    public void SetStepActive(bool isActive)
    {
        unitObjects.ForEach(item => item.SetUnitActive(isActive));
        gameObject.SetActive(isActive);
    }

    public void SelectStep() => stepIndexBox.IsSelected = true;

    public void DestroyStepObject()
    {
        stepIndexBox.DestroyIndexBox();
        Destroy(gameObject);
    }

    public void AddUnit(Unit unitConainer)
    {
        GameObject unitObject = Resources.Load<GameObject>($"_UnitPrefab/{unitConainer.UnitClass}");

        var asset = Instantiate(unitObject, transform).GetComponent<UnitContainer>();
        asset.InitContainer(unitConainer);

        unitObjects.Add(asset);
    }

    public void PlaySimulateStep()
    {
        unitObjects.ForEach(x => x.PlaySimulateUnit());
    }

    void ListenSelectedStep() => OnSend_SeletedStepNumber?.Invoke(StepNum);
    void ListenChangeOrder(int fromIndex, int toIndex) => OnSend_ChangeStepOrder?.Invoke(fromIndex, toIndex);
}
