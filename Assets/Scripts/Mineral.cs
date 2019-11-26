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
    public MineralType Type {
        get { return type; }
    }

    [SerializeField]
    private MineralType type;

}

