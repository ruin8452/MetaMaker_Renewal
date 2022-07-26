using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextValueDragging : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public TMP_InputField TargetText;
    public float DeltaChange = 0.1f;

    float currValue;

    public void OnBeginDrag(PointerEventData eventData)
    {
        currValue = float.Parse(TargetText.text);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 미세 조정
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (eventData.delta.x > 0)
                currValue += DeltaChange * 0.1f;
            else if (eventData.delta.x < 0)
                currValue -= DeltaChange * 0.1f;
        }
        // 대폭 조정
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (eventData.delta.x > 0)
                currValue += DeltaChange * 10f;
            else if (eventData.delta.x < 0)
                currValue -= DeltaChange * 10f;
        }
        // 일반 조정
        else
        {
            if (eventData.delta.x > 0)
                currValue += DeltaChange;
            else if (eventData.delta.x < 0)
                currValue -= DeltaChange;
        }

        TargetText.text = currValue.ToString("0.##");
    }
}
