using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimulContainer : MonoBehaviour
{
    public GameObject StepObjectPrefab;
    public int HandlingStepNum;

    List<StepContainer> stepObjects = new List<StepContainer>();

    private void Awake()
    {

    }

    private void Start()
    {
        AddStep();
    }

    public void OnClick_AddStep() => AddStep();
    void AddStep()
    {
        var newStep = Instantiate(StepObjectPrefab, gameObject.transform).GetComponent<StepContainer>();
        newStep.SetStepNum(stepObjects.Count);
        newStep.OnSend_SeletedStepNumber.AddListener(ListenSelectedStep);
        newStep.OnSend_ChangeStepOrder.AddListener(ListenChangeStepOrder);

        stepObjects.Add(newStep);
        stepObjects[HandlingStepNum].SelectStep();
    }


    public void OnClick_DeleteStep() => DeleteStep();
    void DeleteStep()
    {
        if (stepObjects.Count == 1)
            return;

        stepObjects[HandlingStepNum].DestroyStepObject();
        stepObjects.RemoveAt(HandlingStepNum);

        if (HandlingStepNum >= stepObjects.Count)
            HandlingStepNum = stepObjects.Count - 1;

        // ���õ� Toogle ��ư�� ���� �� ó���� Toggle ��ư���� ���õǴ� ������ ����
        // ���� ���� ���� index�� ����� ��� ���õǰ� �ؾ���
        stepObjects[HandlingStepNum].SelectStep();

        ResetStepsIndex();
    }

    public void OnToggle_DoLoop(bool doLoop) => stepObjects[HandlingStepNum].DoLoop = doLoop;

    void ResetStepsIndex()
    {
        for (int index = 0; index < stepObjects.Count; index++)
        {
            stepObjects[index].SetStepNum(index);
        }
    }

    public void ListenSelectedStep(int stepNum)
    {
        HandlingStepNum = stepNum;


        stepObjects.ForEach(step => step.SetStepActive(false));
        stepObjects[HandlingStepNum].SetStepActive(true);
    }

    public void ListenChangeStepOrder(int fromIndex, int toIndex)
    {
        var tempStep = stepObjects[fromIndex];
        stepObjects.RemoveAt(fromIndex);

        stepObjects.Insert(toIndex, tempStep);

        ResetStepsIndex();
    }

    public void ListenAddUnit(Unit UnitObject)
    {
        stepObjects[HandlingStepNum].AddUnit(UnitObject);
    }


     

    public void OnClick_PlaySimulate()
    {
        stepObjects[HandlingStepNum].PlaySimulateStep();
    }
    public void OnClick_StopSimulate()
    {
        stepObjects[HandlingStepNum].StopSimulateStep();
    }
}
