using System.Collections;
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

        int count = 0;
        foreach (MineralPanelData mineral in minerals)
        {
            MineralPanel panel = Instantiate(template, transform);
            var rect = panel.GetComponent<RectTransform>();
            Vector3 pos = rect.position;
            float width = rect.sizeDelta.x;
            rect.position = new Vector3(pos.x + width * count, pos.y, pos.z);
            inventory[mineral.Type] = 0;
            panels[mineral.Type] = panel;
            panel.Init(mineral);
            count++;
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
