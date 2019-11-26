using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MineralPanelData : ScriptableObject
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