using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralInventoryPanel : MonoBehaviour
{

    [SerializeField]
    private MineralPanel template;
    [SerializeField]
    private List<MineralPanelData> minerals;

    private void Start()
    {
        foreach (MineralPanelData mineral in minerals)
        {
            MineralPanel panel = Instantiate(template, transform);
            panel.Init(mineral);
        }
    }

}
