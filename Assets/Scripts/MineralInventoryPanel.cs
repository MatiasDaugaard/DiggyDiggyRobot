﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralInventoryPanel : MonoBehaviour
{
    [SerializeField]
    private MineralPanel template;
    [SerializeField]
    private List<MineralPanelData> minerals;

    private Dictionary<MineralType, MineralPanel> panels;
    private Dictionary<MineralType, int> inventory;

    private void Start()
    {
        panels = new Dictionary<MineralType, MineralPanel>();
        inventory = new Dictionary<MineralType, int>();
        foreach (MineralPanelData mineral in minerals)
        {
            MineralPanel panel = Instantiate(template, transform);
            inventory[mineral.Type] = 1000;
            panels[mineral.Type] = panel;
            panel.Init(mineral);
        }
    }

    public void Remove(MineralType mineral)
    {
        Remove(mineral, 1);
    }

    public void Remove(MineralType mineral, int amount)
    {
        Add(mineral, -amount);
    }

    public void Add(MineralType mineral)
    {
        Add(mineral, 1);
    }

    public void Add(MineralType mineral, int amount)
    {
        if (inventory.ContainsKey(mineral))
        {
            inventory[mineral] = inventory[mineral] + amount;
            panels[mineral].SetValue(inventory[mineral]);
        }
    }

    public int Get(MineralType mineral)
    {
        return inventory[mineral];
    }

}
