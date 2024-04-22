using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one BuildManager in sceene !");
        instance = this;
    }

    public GameObject standardMachineGunPrefab;
    public GameObject standardMissileLauncher;

    public GameObject buildEffect;

    private TurretBlueprint machineGunToBuild;

    public bool CanBuild { get { return machineGunToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= machineGunToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        machineGunToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.money < machineGunToBuild.cost)
        {
            Debug.Log("Not enough money to build that !");
            return;
        }

        PlayerStats.money -= machineGunToBuild.cost;

        GameObject turret = (GameObject)Instantiate(machineGunToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build ! Money left: " + PlayerStats.money);
    }
}
