using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerTarget : MonoBehaviour
{
    public ColorPicker Picker;
    public Image TargetImage;

    // Start is called before the first frame update
    void Start()
    {
        Picker.onValueChanged.AddListener(color => TargetImage.color = color);
    }
}
