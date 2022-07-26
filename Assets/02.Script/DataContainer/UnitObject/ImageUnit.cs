using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUnit : UnitContainer
{
    public Image ObjectImage;
    Sprite image;
    public Sprite Image
    {
        get { return image; }
        set { ObjectImage.sprite = image = value; }
    }
}
