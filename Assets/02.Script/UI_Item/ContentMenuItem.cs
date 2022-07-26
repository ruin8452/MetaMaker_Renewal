using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContentMenuItem : MonoBehaviour
{
    public Image IconImage;
    public TMP_Text UnitNameText;

    Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
        set { IconImage.sprite = icon = value; }
    }

    string unitName;
    public string UnitName
    {
        get { return unitName; }
        set 
        {
            UnitNameText.text = unitName = value;
            AssetsContainerManager.GetInstance().AddImageAsset(unitName);
        }
    }

    public UnitClasses UnitClass;

    [HideInInspector] public UnityEvent<Unit> OnClick_SelectedObject;

    private void Awake()
    {
        OnClick_SelectedObject.AddListener(GameObject.FindGameObjectWithTag(Cache.TAG_SIMUL_OBJECT).GetComponent<SimulContainer>().ListenAddUnit);
    }


    public void OnClick_InstantiateObject()
    {
        var unit = new Unit
        {
            UnitName = unitName,
            UnitClass = UnitClass
        };

        OnClick_SelectedObject?.Invoke(unit);
    }

    public void OnClick_DeleteItem()
    {
        AssetsContainerManager.GetInstance().DeleteImageAsset(unitName);
        Destroy(gameObject);
    }
}
