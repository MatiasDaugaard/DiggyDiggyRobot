using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MineralPanel : System.Object
{
    public MineralType Type {
        get { return type; }
    }

    public string Name {
        get { return type.ToString(); }
    }

    public Sprite Icon {
        get { return icon; }
    }

    [SerializeField]
    private MineralType type;
    [SerializeField]
    private Sprite icon;
}