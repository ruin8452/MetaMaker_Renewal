using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimulDataContainer : IJsonContainer
{
    public string SimulationName;
    public List<Step> Steps;
}

[Serializable]
public class Step
{
    public bool DoLoop;
    public List<Unit> Assets;
}

[Serializable]
public class Unit
{
    public string UnitName;
    public UnitClasses UnitClass;
    public Color UnitColor;
    public bool IsBG;
    public bool IsVisible;
    public bool IsLock;

    public string ImageFileName;
}

public enum UnitClasses
{
    ModelUnit,
    ImageUnit
}