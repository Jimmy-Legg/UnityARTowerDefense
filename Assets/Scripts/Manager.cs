using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject Level2MachineGunPrefab;

    private GameObject machineGunToBuild;


    public GameObject GetMachineGunToBuild()
    {
        return machineGunToBuild;
    }

    public void SetMachineGunToBuild(GameObject machineGun)
    {
        machineGunToBuild = machineGun;
    }
}
