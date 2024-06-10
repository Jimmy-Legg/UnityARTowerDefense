using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DataManager;

public class UpgradeShop : MonoBehaviour
{
    public DataManager dataManager;
    private DataManager.MyData data;

    public TurretsDataManager turretDataManager;
    private TurretsDataManager.MyData turretData;

    [Header("Money")]
    [SerializeField] private Text moneyText;

    [Header("Buy Buttons")]
    public Button MachineGunLvl2BuyButton;
    public Button MissileLauncherLvl1BuyButton;
    public Button MissileLauncherLvl2BuyButton;
    public Button LaserGunLvl1BuyButton;

    [Header("Slider MachinGun")]
    public Slider machineGunLvl1Slider;
    public Slider machineGunLvl2Slider;

    [Header("Slider MissileLauncher")]
    public Slider missileLauncherLvl1Slider;
    public Slider missileLauncherLvl2Slider;


    [Header("Price Tooltip")]
    public GameObject priceTooltip;
    public TextMeshProUGUI priceText;

    public void Start()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        turretDataManager = (TurretsDataManager)FindFirstObjectByType(typeof(TurretsDataManager));

        moneyText.text = "Money: " + dataManager.LoadData().Money.ToString();

        data = dataManager.LoadData();
        turretData = turretDataManager.LoadData();

        InitializeSliders();

        //if turret is buyed in data then change button text to selected
        if (data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }

        if (data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }

        if (data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }

        if (data.MissileLauncherLvl2)
        {
            IsBuyed(MissileLauncherLvl2BuyButton);
        }

    }

    public void ShowPriceTooltip(string price, Vector3 position)
    {
        priceText.text = "$" + price;
        priceTooltip.transform.position = position + new Vector3(0, 30, 0); ;
        priceTooltip.SetActive(true);
    }

    public void HidePriceTooltip()
    {
        priceTooltip.SetActive(false);
    }

    private void InitializeSliders()
    {
        turretData = turretDataManager.LoadData();
        machineGunLvl1Slider.maxValue = turretData.fireRateUpgradesMachineGunLvl1[turretData.fireRateUpgradesMachineGunLvl1.Length - 1];
        machineGunLvl1Slider.value = turretData.lastMachineGunLvl1FireRateBuyed;

        machineGunLvl2Slider.maxValue = turretData.fireRateUpgradesMachineGunLvl2[turretData.fireRateUpgradesMachineGunLvl2.Length - 1];
        machineGunLvl2Slider.value = turretData.lastMachineGunLvl2FireRateBuyed;

        missileLauncherLvl1Slider.maxValue = turretData.fireRateUpgradesMissileLauncherLvl1[turretData.fireRateUpgradesMissileLauncherLvl1.Length - 1];
        missileLauncherLvl1Slider.value = turretData.lastMissileLauncherLvl1FireRateBuyed;

        missileLauncherLvl2Slider.maxValue = turretData.fireRateUpgradesMissileLauncherLvl2[turretData.fireRateUpgradesMissileLauncherLvl2.Length - 1];
        missileLauncherLvl2Slider.value = turretData.lastMissileLauncherLvl2FireRateBuyed;
    }

    public void BuyMachineGunLvl2()
    {
        bool isBuyed = dataManager.BuyMachineGunLvl2();
        if (isBuyed || data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }
    }

    public void BuyMissileLauncher()
    {
        bool isBuyed = dataManager.BuyMissileLauncher();
        if (isBuyed || data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }
    }
    public void BuyMissileLauncherLvl2()
    {
        bool isBuyed = dataManager.BuyRocketLauncherLvl2();
        if (isBuyed || data.MissileLauncherLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }
    }
    public void BuyLaserGun()
    {
        bool isBuyed = dataManager.BuyLaserGun();
        if (isBuyed || data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }
    }

    // Upgrade turret fire rate methods
    public void UpgradeMachineGunLvl1()
    {
        int moneySpent = 50;

        turretDataManager.UpgradeTurret("MachineGun", 1, moneySpent);

        UpdateSliders();
    }

    public void UpgradeMachineGunLvl2()
    {
        if (!data.MachineGunLvl2)
            return;
        int moneySpent = 100;

        turretDataManager.UpgradeTurret("MachineGun", 2, moneySpent);

        UpdateSliders();
    }

    public void UpgradeMissileLauncher()
    {
        if (!data.MissileLauncher)
            return;
        int moneySpent = 200;

        turretDataManager.UpgradeTurret("MissileLauncher", 1, moneySpent);

        UpdateSliders();

    }

    public void UpgradeMissileLauncherLvl2()
    {
        if (!data.MissileLauncherLvl2)
            return;
        int moneySpent = 400;

        turretDataManager.UpgradeTurret("MissileLauncher", 2, moneySpent);

        UpdateSliders();
    }


    public void Update()
    {
        moneyText.text = "Money: " + dataManager.LoadData().Money.ToString();
        data = dataManager.LoadData();
        UpdateSliders();


    }

    private void UpdateSliders()
    {
        turretData = turretDataManager.LoadData();

        machineGunLvl1Slider.value = turretData.lastMachineGunLvl1FireRateBuyed;

        machineGunLvl2Slider.value = turretData.lastMachineGunLvl2FireRateBuyed;

        missileLauncherLvl1Slider.value = turretData.lastMissileLauncherLvl1FireRateBuyed;

        missileLauncherLvl2Slider.value = turretData.lastMissileLauncherLvl2FireRateBuyed;

        MyData data = dataManager.LoadData();

        if (data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }

        if (data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }

        if (data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }

        if (data.MissileLauncherLvl2)
        {
            IsBuyed(MissileLauncherLvl2BuyButton);
        }

    }

    public void ResetData()
    {
        dataManager.ResetData();
    }

    public void ResetDataTurret()
    {
        turretDataManager.ResetData();
    }

    public void IsBuyed(Button button)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "Buyed";
        }
        else
        {
            Debug.LogWarning("Text component not found as a child of the button.");
        }
    }
}
