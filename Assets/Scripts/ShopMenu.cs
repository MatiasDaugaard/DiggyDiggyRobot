using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{

    public Button drillButton;
    public Button armorButton;
    public Button jetpowerButton;
    public Button fueltankButton;
    public Button weaponButton;
    public Button refuelButton;
    public Button leaveButton;

    public Button firstUpgrade;
    public Button secondUpgrade;
    public Button thirdUpgrade;
    public Button fourthUpgrade;
    public Button fifthUpgrade;
    public Button sixthUpgrade;

    public Button doneButton;

    void Start()
    {
        drillButton.GetComponentInChildren<Text>().text = "Upgrade Drill";
        drillButton.onClick.AddListener(DrillOnClick);
        armorButton.GetComponentInChildren<Text>().text = "Upgrade Armor";
        armorButton.onClick.AddListener(ArmorOnClick);
        jetpowerButton.GetComponentInChildren<Text>().text = "Upgrade Jetpack";
        jetpowerButton.onClick.AddListener(JetOnClick);
        fueltankButton.GetComponentInChildren<Text>().text = "Upgrade Fueltank";
        fueltankButton.onClick.AddListener(FuelOnClick);
        weaponButton.GetComponentInChildren<Text>().text = "Upgrade Weapon";
        weaponButton.onClick.AddListener(WeaponOnClick);
        refuelButton.GetComponentInChildren<Text>().text = "Refuel";
        refuelButton.onClick.AddListener(RefuelOnClick);
        leaveButton.GetComponentInChildren<Text>().text = "Leave";
        leaveButton.onClick.AddListener(LeaveOnClick);
        drillButton.gameObject.SetActive(true);
        armorButton.gameObject.SetActive(true);
        jetpowerButton.gameObject.SetActive(true);
        fueltankButton.gameObject.SetActive(true);
        weaponButton.gameObject.SetActive(true);
        refuelButton.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);
        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        fourthUpgrade.gameObject.SetActive(false);
        fifthUpgrade.gameObject.SetActive(false);
        sixthUpgrade.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);


    }

    void Upgrade(bool[] upgrades, string equipment)
    {
        drillButton.gameObject.SetActive(false); 
        armorButton.gameObject.SetActive(false);
        jetpowerButton.gameObject.SetActive(false);
        fueltankButton.gameObject.SetActive(false);
        weaponButton.gameObject.SetActive(false);
        refuelButton.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(false);
        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        fourthUpgrade.gameObject.SetActive(false);
        fifthUpgrade.gameObject.SetActive(false);
        sixthUpgrade.gameObject.SetActive(false);

        if (!upgrades[0])
        {
            firstUpgrade.GetComponentInChildren<Text>().text = "Copper upgrade : 10 copper";
            firstUpgrade.onClick.AddListener(delegate { UpgradeOnClick(0, equipment); });
            firstUpgrade.gameObject.SetActive(true);
        }
        if (!upgrades[1])
        {
            secondUpgrade.GetComponentInChildren<Text>().text = "Bronze upgrade : 15 copper, 5 iron";
            secondUpgrade.onClick.AddListener(delegate { UpgradeOnClick(1, equipment); });
            secondUpgrade.gameObject.SetActive(true);
        }
        if (!upgrades[2])
        {
            thirdUpgrade.GetComponentInChildren<Text>().text = "Iron upgrade : 20 iron, 5 copper";
            thirdUpgrade.onClick.AddListener(delegate { UpgradeOnClick(2, equipment); });
            thirdUpgrade.gameObject.SetActive(true);
        }
        if (!upgrades[3])
        {
            fourthUpgrade.GetComponentInChildren<Text>().text = "Silver upgrade : 25 iron, 5 titanium";
            fourthUpgrade.onClick.AddListener(delegate { UpgradeOnClick(3, equipment); });
            fourthUpgrade.gameObject.SetActive(true);
        }
        if (!upgrades[4])
        {
            fifthUpgrade.GetComponentInChildren<Text>().text = "Platinum upgrade : 20 titanium, 15 iron, 10 copper";
            fifthUpgrade.onClick.AddListener(delegate { UpgradeOnClick(4, equipment); });
            fifthUpgrade.gameObject.SetActive(true);
        }
        if (!upgrades[5])
        {
            sixthUpgrade.GetComponentInChildren<Text>().text = "Titanium upgrade : 100 titanium";
            sixthUpgrade.onClick.AddListener(delegate { UpgradeOnClick(5, equipment); });
            sixthUpgrade.gameObject.SetActive(true);
        }
        doneButton.GetComponentInChildren<Text>().text = "Done";
        doneButton.onClick.AddListener(DoneOnClick);
        doneButton.gameObject.SetActive(true);
        


    }

    private void UpgradeOnClick(int no, string equipment)
    {
        bool[] upgrades = { };
        switch (equipment)
        {
            case "Drill":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpgradeDrill(no);
                upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDrillUpgrades();
                Upgrade(upgrades, "Drill");
                break;
            case "Armor":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpgradeArmor(no);
                upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetArmorUpgrades();
                Upgrade(upgrades, "Armor");
                break;
            case "Jet":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpgradeJet(no);
                upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetJetUpgrades();
                Upgrade(upgrades, "Jet");
                break;
            case "Fuel":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpgradeFuel(no);
                upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetFuelUpgrades();
                Upgrade(upgrades, "Fuel");
                break;
            case "Weapon":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpgradeWeapon(no);
                upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetWeaponUpgrades();
                Upgrade(upgrades, "Weapon");
                break;
            default:
                break;
        }
        
    }

    private void DoneOnClick()
    {
        
        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        fourthUpgrade.gameObject.SetActive(false);
        fifthUpgrade.gameObject.SetActive(false);
        sixthUpgrade.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);
        drillButton.gameObject.SetActive(true);
        armorButton.gameObject.SetActive(true);
        jetpowerButton.gameObject.SetActive(true);
        fueltankButton.gameObject.SetActive(true);
        weaponButton.gameObject.SetActive(true);
        refuelButton.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);
        
    }

    void DrillOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDrillUpgrades();
        Upgrade(upgrades, "Drill");
    }

    void ArmorOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetArmorUpgrades();
        Upgrade(upgrades, "Armor");
    }

    void JetOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetJetUpgrades();
        Upgrade(upgrades, "Jet");
    }

    void FuelOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetFuelUpgrades();
        Upgrade(upgrades, "Fuel");
    }

    void WeaponOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetWeaponUpgrades();
        Upgrade(upgrades, "Weapon");
    }

    void RefuelOnClick()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Refuel();
    }


    void LeaveOnClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
