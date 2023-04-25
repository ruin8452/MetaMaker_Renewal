using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image TargetImage;

    public Sprite NormalImage;
    public Color NormalImageColor;

    public Sprite ChangedImage;
    public Color ChangedImageColor;

    public void ChangeImage(bool isSignal)
    {
        TargetImage.sprite = isSignal ? ChangedImage : NormalImage;
        TargetImage.color = isSignal ? ChangedImageColor : NormalImageColor;
    }
}
