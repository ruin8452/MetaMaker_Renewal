using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentBaseInfo : MonoBehaviour, IComponent
{
    public TMP_InputField NameText;
    public Image ColorImage;
    public bool isBackGround;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
