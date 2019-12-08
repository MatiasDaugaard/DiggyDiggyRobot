using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        drillButton.GetComponentInChildren<Text>().text = "Upgrade Drill";
        drillButton.onClick.AddListener(DrillOnClick);
        armorButton.GetComponentInChildren<Text>().text = "Upgrade Armor";
        armorButton.onClick.AddListener(ArmorOnClick);
        jetpowerButton.GetComponentInChildren<Text>().text = "Upgrade Jetpack";
        jetpowerButton.onClick.AddListener(JetOnClick);
        fueltankButton.GetComponentInChildren<Text>().text = "Upgrade Fueltank";
        fueltankButton.onClick.AddListener(TankOnClick);
        weaponButton.GetComponentInChildren<Text>().text = "Upgrade Weapon";
        weaponButton.onClick.AddListener(WeaponOnClick);
        refuelButton.GetComponentInChildren<Text>().text = "Refuel";
        refuelButton.onClick.AddListener(RefuelOnClick);
        leaveButton.GetComponentInChildren<Text>().text = "Leave";
        leaveButton.onClick.AddListener(LeaveOnClick);

        
    }

    void DrillOnClick()
    {
        bool[] upgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getDrillUpgrades();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().upgradeDrill();
    }

    void ArmorOnClick()
    {
        //Open other menu
    }

    void JetOnClick()
    {
        //Open other menu
    }

    void TankOnClick()
    {
        //Open other menu
    }

    void WeaponOnClick()
    {
        //Open other menu
    }

    void RefuelOnClick()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().refuel();
    }


    void LeaveOnClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
