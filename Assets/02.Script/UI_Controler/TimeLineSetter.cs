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

    public float DefaultTime = 3f;

    public Slider Indicate;

    public static float RunningTime;
    public static float CurrTime;
    readonly string TIME_FORMAT = "0.000s";
    float preTime;

    bool doLoop;

    Coroutine SimulationCoroutine;

    [HideInInspector] public UnityEvent<float> RunTimeChangeEvent;
    [HideInInspector] public UnityEvent<float> CurrTimeChangeEvent;

    private void Awake()
    {
        RunningTime = DefaultTime;
        CurrTime = 0f;
    }

    private void Start()
    {
        SetStepRunTimeText();
        SetTimeLineText();
    }

    //-----------------------------------------------------

    public void OnSelecte_InputText(string text)
    {
        text = text.TrimEnd('s');
        preTime = float.Parse(text);
    }

    public void OnEndEdit_StepRunTimeText(string inputText)
    {
        inputText = inputText.TrimEnd('s');
        bool result = float.TryParse(inputText, out RunningTime);

        if (!result || RunningTime <= 0)
            RunningTime = preTime;

        SetStepRunTimeText();
        SetTimeLineText();

        Indicate.value = CurrTime / RunningTime;
        RunTimeChangeEvent?.Invoke(RunningTime);
    }

    void SetStepRunTimeText()
    {
        StepRunTimeText.text = RunningTime.ToString(TIME_FORMAT);
    }

    void SetTimeLineText()
    {
        for (int i = 0; i < TimeLineTexts.Count; i++)
            TimeLineTexts[i].text = (RunningTime * 0.1 * i).ToString(TIME_FORMAT);
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

        MoveIndicate(CurrTime);
        SetCurrTimeText();

        CurrTimeChangeEvent?.Invoke(Indicate.value);
    }

    public void OnValueChange_MoveIndicate(float percent)
    {
        CurrTime = RunningTime * percent;
        SetCurrTimeText();
        
        CurrTimeChangeEvent?.Invoke(percent);
    }

    void SetCurrTimeText()
    {
        CurrTimeText.text = CurrTime.ToString(TIME_FORMAT);
    }

    void MoveIndicate(float currTime) => Indicate.value = currTime / RunningTime;

    public void OnToggle_DoLoop(bool doLoop) => this.doLoop = doLoop;


    // --------------------------------------------------------------
    // Simulator-----------------------------------------------------
    public void OnClick_PlaySimulate()
    {
        if (SimulationCoroutine != null)
            StopCoroutine(SimulationCoroutine);

        SimulationCoroutine = StartCoroutine(PlaySimulate(doLoop));
    }
    public void OnClick_StopSimulate()
    {
        if(SimulationCoroutine != null)
            StopCoroutine(SimulationCoroutine);
    }

    IEnumerator PlaySimulate(bool doLoop)
    {
        do
        {
            CurrTime = 0f;

            SetCurrTimeText();
            MoveIndicate(CurrTime);

            while (CurrTime <= RunningTime)
            {
                CurrTime += Time.deltaTime;

                SetCurrTimeText();
                MoveIndicate(CurrTime);

                yield return null;
            }
        } while (doLoop);

        CurrTime = 0f;
        SetCurrTimeText();
        MoveIndicate(0f);
    }
}
