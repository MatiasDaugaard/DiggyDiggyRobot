using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum MineralType
{
    Empty,
    Solid,
    Ground,
    Copper,
    Iron,
    Titanium
}

[Serializable]
public class Mineral : MonoBehaviour
{
    [SerializeField]
    private MineralType type;
    [SerializeField]
    private int[] location;

    public MineralType Type
    {
        get { return type; }
    }

    public int[] Location
    {
        get { return location; }
        set { location = value; }
    }

}

