﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    // Controller
    private PlayerController pc;

    // Player data
    private float health;
    public float maxHealth;
    public float speedMultiplier;
    private float fuel;
    public float maxFuel;
    public float weaponDamage;
    public int miningSpeed;
    public int mobKills;
    public bool bossKilled;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            pc.UpdateHealthBar();
        }
    }

    public float Fuel
    {
        get { return fuel; }
        set
        {
            fuel = value;
            pc.UpdateFuelBar();
        }
    }

    //Equipment variables
    [SerializeField]
    private bool[] armorUpgrades = { false, false, false, false, false, false };
    [SerializeField]
    private bool[] fuelUpgrades = { false, false, false, false, false, false };
    [SerializeField]
    private bool[] jetUpgrades = { false, false, false, false, false, false };
    [SerializeField]
    private bool[] weaponUpgrades = { false, false, false, false, false, false };
    [SerializeField]
    private bool[] drillUpgrades = { false, false, false, false, false, false };

    public bool[] ArmorUpgrades
    {
        get { return armorUpgrades; }
        set { armorUpgrades = value; }
    }
    public bool[] FuelUpgrades
    {
        get { return fuelUpgrades; }
        set { fuelUpgrades = value; }
    }
    public bool[] JetUpgrades
    {
        get { return jetUpgrades; }
        set { jetUpgrades = value; }
    }
    public bool[] WeaponUpgrades
    {
        get { return weaponUpgrades; }
        set { weaponUpgrades = value; }
    }
    public bool[] DrillUpgrades
    {
        get { return drillUpgrades; }
        set { drillUpgrades = value; }
    }

    public Player(PlayerController playerController)
    {
        health = 20.0f;
        maxHealth = 20.0f;
        fuel = 20.0f;
        maxFuel = 20.0f;
        speedMultiplier = 1.0f;
        weaponDamage = 5.0f;
        miningSpeed = 40;
        mobKills = 0;
        bossKilled = false;
        pc = playerController;

    }

    public void Damage(float dmg)
    {
        health -= dmg;
        pc.UpdateHealthBar();
    }


    public void Refuel()
    {
        Fuel = maxFuel;
    }

    public void Repair()
    {
        Health = maxHealth;
    }

    public void Move()
    {
        Fuel -= 0.01f;
    }

    private bool UpgradeEquipment(string upgrade)
    {
        switch (upgrade)
        {
            case "Copper":
                if (pc.inventory.Get(MineralType.Copper) >= 10)
                {
                    pc.inventory.Remove(MineralType.Copper, 10);
                    return true;
                }
                return false;
            case "Bronze":
                if (pc.inventory.Get(MineralType.Copper) >= 15 && pc.inventory.Get(MineralType.Iron) >= 5)
                {
                    pc.inventory.Remove(MineralType.Copper, 15);
                    pc.inventory.Remove(MineralType.Iron, 5);
                    return true;
                }
                return false;

            case "Iron":
                if (pc.inventory.Get(MineralType.Copper) >= 5 && pc.inventory.Get(MineralType.Iron) >= 20)
                {
                    pc.inventory.Remove(MineralType.Copper, 5);
                    pc.inventory.Remove(MineralType.Iron, 20);
                    return true;
                }
                return false;

            case "Silver":
                if (pc.inventory.Get(MineralType.Iron) >= 25 && pc.inventory.Get(MineralType.Titanium) >= 5)
                {
                    pc.inventory.Remove(MineralType.Iron, 25);
                    pc.inventory.Remove(MineralType.Titanium, 5);
                    return true;
                }
                return false;

            case "Platinum":
                if (pc.inventory.Get(MineralType.Copper) >= 10 && pc.inventory.Get(MineralType.Iron) >= 15 && pc.inventory.Get(MineralType.Titanium) >= 20)
                {
                    pc.inventory.Remove(MineralType.Copper, 10);
                    pc.inventory.Remove(MineralType.Iron, 15);
                    pc.inventory.Remove(MineralType.Titanium, 20);
                    return true;
                }
                return false;

            case "Titanium":
                if (pc.inventory.Get(MineralType.Titanium) >= 100)
                {
                    pc.inventory.Remove(MineralType.Titanium, 100);
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    public void UpgradeDrill(int no)
    {
        if (!drillUpgrades[no])
        {
            switch (no)
            {
                case 0:
                    if (UpgradeEquipment("Copper"))
                    {
                        miningSpeed -= 2;
                        drillUpgrades[no] = true;
                    }
                    break;
                case 1:
                    if (UpgradeEquipment("Bronze"))
                    {
                        miningSpeed -= 3;
                        drillUpgrades[no] = true;
                    }
                    break;
                case 2:
                    if (UpgradeEquipment("Iron"))
                    {
                        miningSpeed -= 5;
                        drillUpgrades[no] = true;
                    }
                    break;
                case 3:
                    if (UpgradeEquipment("Silver"))
                    {
                        miningSpeed -= 5;
                        drillUpgrades[no] = true;
                    }
                    break;
                case 4:
                    if (UpgradeEquipment("Platinum"))
                    {
                        miningSpeed -= 7;
                        drillUpgrades[no] = true;
                    }
                    break;
                case 5:
                    if (UpgradeEquipment("Titanium"))
                    {
                        miningSpeed -= 8;
                        drillUpgrades[no] = true;
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void UpgradeArmor(int no)
    {
        if (!armorUpgrades[no])
        {
            switch (no)
            {
                case 0:
                    if (UpgradeEquipment("Copper"))
                    {
                        maxHealth += 10;
                        armorUpgrades[no] = true;
                    }
                    break;
                case 1:
                    if (UpgradeEquipment("Bronze"))
                    {
                        maxHealth += 15;
                        armorUpgrades[no] = true;
                    }
                    break;
                case 2:
                    if (UpgradeEquipment("Iron"))
                    {
                        maxHealth += 25;
                        armorUpgrades[no] = true;
                    }
                    break;
                case 3:
                    if (UpgradeEquipment("Silver"))
                    {
                        maxHealth += 25;
                        armorUpgrades[no] = true;
                    }
                    break;
                case 4:
                    if (UpgradeEquipment("Platinum"))
                    {
                        maxHealth += 35;
                        armorUpgrades[no] = true;
                    }
                    break;
                case 5:
                    if (UpgradeEquipment("Titanium"))
                    {
                        maxHealth += 40;
                        armorUpgrades[no] = true;
                    }
                    break;
                default:
                    break;
            }
            pc.UpdateHealthBar();

        }
    }

    public void UpgradeJet(int no)
    {
        if (!jetUpgrades[no])
        {
            switch (no)
            {
                case 0:
                    if (UpgradeEquipment("Copper"))
                    {
                        speedMultiplier += 0.1f;
                        jetUpgrades[no] = true;
                    }
                    break;
                case 1:
                    if (UpgradeEquipment("Bronze"))
                    {
                        speedMultiplier += 0.15f;
                        jetUpgrades[no] = true;
                    }
                    break;
                case 2:
                    if (UpgradeEquipment("Iron"))
                    {
                        speedMultiplier += 0.2f;
                        jetUpgrades[no] = true;
                    }
                    break;
                case 3:
                    if (UpgradeEquipment("Silver"))
                    {
                        speedMultiplier += 0.25f;
                        jetUpgrades[no] = true;
                    }
                    break;
                case 4:
                    if (UpgradeEquipment("Platinum"))
                    {
                        speedMultiplier += 0.3f;
                        jetUpgrades[no] = true;
                    }
                    break;
                case 5:
                    if (UpgradeEquipment("Titanium"))
                    {
                        speedMultiplier += 0.5f;
                        jetUpgrades[no] = true;
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void UpgradeFuel(int no)
    {
        if (!fuelUpgrades[no])
        {
            switch (no)
            {
                case 0:
                    if (UpgradeEquipment("Copper"))
                    {
                        maxFuel += 10;
                        fuelUpgrades[no] = true;
                    }
                    break;
                case 1:
                    if (UpgradeEquipment("Bronze"))
                    {
                        maxFuel += 15;
                        fuelUpgrades[no] = true;
                    }
                    break;
                case 2:
                    if (UpgradeEquipment("Iron"))
                    {
                        maxFuel += 25;
                        fuelUpgrades[no] = true;
                    }
                    break;
                case 3:
                    if (UpgradeEquipment("Silver"))
                    {
                        maxFuel += 25;
                        fuelUpgrades[no] = true;
                    }
                    break;
                case 4:
                    if (UpgradeEquipment("Platinum"))
                    {
                        maxFuel += 35;
                        fuelUpgrades[no] = true;
                    }
                    break;
                case 5:
                    if (UpgradeEquipment("Titanium"))
                    {
                        maxFuel += 40;
                        fuelUpgrades[no] = true;
                    }
                    break;
                default:
                    break;
            }
            pc.UpdateFuelBar();

        }
    }

    public void UpgradeWeapon(int no)
    {
        if (!weaponUpgrades[no])
        {
            switch (no)
            {
                case 0:
                    if (UpgradeEquipment("Copper"))
                    {
                        weaponDamage += 2;
                        weaponUpgrades[no] = true;
                    }
                    break;
                case 1:
                    if (UpgradeEquipment("Bronze"))
                    {
                        weaponDamage += 3;
                        weaponUpgrades[no] = true;
                    }
                    break;
                case 2:
                    if (UpgradeEquipment("Iron"))
                    {
                        weaponDamage += 3;
                        weaponUpgrades[no] = true;
                    }
                    break;
                case 3:
                    if (UpgradeEquipment("Silver"))
                    {
                        weaponDamage += 5;
                        weaponUpgrades[no] = true;
                    }
                    break;
                case 4:
                    if (UpgradeEquipment("Platinum"))
                    {
                        weaponDamage += 5;
                        weaponUpgrades[no] = true;
                    }
                    break;
                case 5:
                    if (UpgradeEquipment("Titanium"))
                    {
                        weaponDamage += 7;
                        weaponUpgrades[no] = true;
                    }
                    break;
                default:
                    break;
            }

        }
    }

}
