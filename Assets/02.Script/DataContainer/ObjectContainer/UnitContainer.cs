using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitContainer : MonoBehaviour
{
    public string UnitName;
    public string UnitClass;
    public Color UnitColor;
    public bool IsBG;
    public bool IsVisible;
    public bool IsLock;

    public List<KeyFrame> KeyFrames = new List<KeyFrame>();

    protected TrackObjectItem trackItem;
    protected Unit unitData;
}

public class KeyFrame : ICloneable
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public float Percent;

    public KeyFrame()
    {
        Position = Vector3.zero;
        Rotation = Vector3.zero;
        Scale = Vector3.one;
        Percent = 0;
    }
    public KeyFrame(float percent)
    {
        Position = Vector3.zero;
        Rotation = Vector3.zero;
        Scale = Vector3.one;
        Percent = percent;
    }

    public object Clone()
    {
        var clone = new KeyFrame
        {
            Position = this.Position,
            Rotation = this.Rotation,
            Scale = this.Scale,
            Percent = this.Percent
        };

        return clone;
    }
}