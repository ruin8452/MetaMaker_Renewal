using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimation : MonoBehaviour
{
    public Toggle TargetToggle;
    public Animator TargetAnimator;
    public string CtrlParameter;

    public void OnClick_Toggle(bool isOn)
    {
        TargetAnimator.SetBool(CtrlParameter, isOn);
    }
}
