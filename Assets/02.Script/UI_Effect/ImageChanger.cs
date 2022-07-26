using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image TargetImage;

    public Sprite NormalImage;
    public Sprite ChangedImage;

    public void ChangeImage(bool isSignal)
    {
        TargetImage.sprite = isSignal ? ChangedImage : NormalImage;
    }
}
