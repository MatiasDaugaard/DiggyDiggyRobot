using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineralPanel : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;

    public void Init(MineralPanelData data)
    {
        image.sprite = data.Icon;
        SetValue(0);
    }

    public void SetValue(int value)
    {
        text.text = value.ToString();
    }
}
