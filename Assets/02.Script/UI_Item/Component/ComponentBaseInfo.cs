using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComponentBaseInfo : MonoBehaviour, IComponent
{
    public TMP_InputField NameText;
    public Image ColorImage;
    public bool isBackGround;

    public ColorPicker Picker;

    string preName;

    public UnityEvent<string> EditNameEvent;

    // Start is called before the first frame update
    void Start()
    {
        Picker.onValueChanged.AddListener(color => ColorImage.color = color);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void OnSelecte_InputName(string name)
    {
        preName = name;
    }

    public void OnEndEdit_InputName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            name = preName;

        EditNameEvent?.Invoke(name);
    }
}
