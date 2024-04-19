using UnityEngine;

public class Shop : MonoBehaviour
{
    Manager manager;

    private void Start()
    {
        manager = Manager.instance;
    }
    public void PurchasedStandardMachineGun()
    {
        Debug.Log("Selected Standard MachineGun");

        manager.SetMachineGunToBuild(manager.standardMachineGunPrefab);
    }

    public void PurchasedLevl2MachineGun()
    {
        Debug.Log("Selected Standard Other");

        manager.SetMachineGunToBuild(manager.Level2MachineGunPrefab);

    }
}
