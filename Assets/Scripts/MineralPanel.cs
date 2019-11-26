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
        text.text = "0";
    }
}
