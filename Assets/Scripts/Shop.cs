using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardMachineGun;
    public TurretBlueprint missileLauncher;
    Manager manager;

    private void Start()
    {
        manager = Manager.instance;
    }
    public void SelectStandardMachineGun()
    {
        Debug.Log("Selected Standard MachineGun");

        manager.SelectTurretToBuild(standardMachineGun);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Selected Standard Other");

        manager.SelectTurretToBuild(missileLauncher);

    }
}
