using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StepIndexBox : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image OrderChangeImage;
    public TMP_Text IndexText;
    public int index;
    public int Index
    {
        get { return index; }
        set
        {
            index = value;
            IndexText.text = (index + 1).ToString();
        }
    }

    Toggle myToogle;
    bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set{ myToogle.isOn = isSelected = value; }
    }

    Transform rootCanvasTr;
    Transform preParentTr;
    RectTransform rectTr;
    CanvasGroup canvasGroup;
    static int ToIndex;

    public Action SelectedStep;
    public Action<int, int> ChangeOrder;

    private void Awake()
    {
        myToogle = GetComponent<Toggle>();
        myToogle.group = GameObject.FindGameObjectWithTag(Cache.TAG_STEPS_AREA).GetComponent<ToggleGroup>();

        rootCanvasTr = GameObject.Find("MainCanvas").transform;
        rectTr = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        preParentTr = transform.parent;
        ToIndex = preParentTr.childCount - 1;   // Step이 추가될때 마다 마지막 인덱스를 가리킴
    }

    public void SetIndex(int indexNum)
    {
        Index = indexNum;
        transform.SetSiblingIndex(Index);
    }

    public void DestroyIndexBox()
    {
        Destroy(gameObject);
    }

    public void OnClick_SelectedStep()
    {
        SelectedStep?.Invoke();
    }








    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(rootCanvasTr);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTr.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(preParentTr);
        rectTr.position = preParentTr.GetComponent<RectTransform>().position;

        ChangeOrder?.Invoke(Index, ToIndex);
        ToIndex = preParentTr.childCount - 1;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ToIndex = Index;

        ChangeImageAlpha(0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.dragging) return;

        ChangeImageAlpha(1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.dragging) return;

        ChangeImageAlpha(0f);
    }

    void ChangeImageAlpha(float alpha)
    {
        var color = OrderChangeImage.color;
        color.a = alpha;
        OrderChangeImage.color = color;
    }
}
