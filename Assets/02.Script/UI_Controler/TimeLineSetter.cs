using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeLineSetter : MonoBehaviour
{
    public TMP_InputField StepRunTimeText;
    public TMP_InputField CurrTimeText;
    public List<TMP_Text> TimeLineTexts;

    public float DefaultTime = 1f;

    public Slider Indicate;

    public static float RunningTime;
    public static float CurrTime;
    float preTime;

    [HideInInspector] public UnityEvent<float, float> ChangeTimeLine;

    Coroutine SimulationCoroutine;

    private void Awake()
    {
        RunningTime = DefaultTime;
        CurrTime = 0f;

        GameObject.FindGameObjectWithTag(Cache.TAG_SIMUL_OBJECT).GetComponent<SimulContainer>().OnPlay_Simaulate.AddListener(ListenPlaySimulate);
        GameObject.FindGameObjectWithTag(Cache.TAG_SIMUL_OBJECT).GetComponent<SimulContainer>().OnStop_Simaulate.AddListener(ListenStopSimulate);
    }

    private void Start()
    {
        SetStepRunTimeText(RunningTime);
        SetTimeLineText(RunningTime);

        ChangeTimeLine?.Invoke(RunningTime, CurrTime);
    }

    //-----------------------------------------------------

    public void OnEndEdit_StepRunTimeText(string inputText)
    {
        inputText = inputText.TrimEnd('s');
        bool result = float.TryParse(inputText, out RunningTime);

        if (!result || RunningTime <= 0)
            RunningTime = preTime;

        SetStepRunTimeText(RunningTime);
        SetTimeLineText(RunningTime);
    }

    void SetStepRunTimeText(float runningTime)
    {
        StepRunTimeText.text = runningTime.ToString("0.00s");
        ChangeTimeLine?.Invoke(runningTime, CurrTime);
    }

    void SetTimeLineText(float runningTime)
    {
        for (int i = 0; i < TimeLineTexts.Count; i++)
            TimeLineTexts[i].text = (runningTime * 0.1 * i).ToString("0.00s");
    }

    public void OnSelecte_InputText(string text)
    {
        text = text.TrimEnd('s');
        preTime = float.Parse(text);
    }

    //-----------------------------------------------------

    public void OnEndEdit_CurrTimeText(string inputText)
    {
        inputText = inputText.TrimEnd('s');

        bool result = float.TryParse(inputText, out CurrTime);

        if (!result || CurrTime <= 0)
            CurrTime = preTime;
        else if (CurrTime > RunningTime)
            CurrTime = RunningTime;

        SetCurrTimeText(CurrTime);
        MoveIndicate(CurrTime);
    }

    void SetCurrTimeText(float currTime)
    {
        CurrTimeText.text = currTime.ToString("0.00s");
        ChangeTimeLine?.Invoke(RunningTime, currTime);
    }

    void MoveIndicate(float currTime) => Indicate.value = currTime / RunningTime;

    public void OnValueChange_MoveIndicate(float percent) => SetCurrTimeText(RunningTime * percent);

    //-----------------------------------------------------

    public void ListenPlaySimulate(bool doLoop)
    {
        if (SimulationCoroutine != null)
            StopCoroutine(SimulationCoroutine);

        SimulationCoroutine = StartCoroutine(PlaySimulate(doLoop));
    }
    public void ListenStopSimulate()
    {
        if(SimulationCoroutine != null)
            StopCoroutine(SimulationCoroutine);
    }

    IEnumerator PlaySimulate(bool doLoop)
    {
        do
        {
            CurrTime = 0f;

            SetCurrTimeText(CurrTime);
            MoveIndicate(CurrTime);

            while (CurrTime < RunningTime)
            {
                CurrTime += Time.deltaTime;

                SetCurrTimeText(CurrTime);
                MoveIndicate(CurrTime);

                yield return null;
            }
        } while (doLoop);


        SetCurrTimeText(0f);
        MoveIndicate(0f);
    }
}
