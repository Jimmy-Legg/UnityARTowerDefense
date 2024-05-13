using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public TurretBlueprint standardMachineGun;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserGun;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one Shop in scene!");
        instance = this;
    }

    public TurretBlueprint selectedTurret;

    public void SelectStandardMachineGun()
    {
        if (selectedTurret == standardMachineGun)
        {
            Debug.Log("Deselected Standard MachineGun");
            Manager.instance.DeselectTurretToBuild();
            selectedTurret = null;
        }
        else
        {
            Debug.Log("Selected Standard MachineGun");
            Manager.instance.SelectTurretToBuild(standardMachineGun);
            selectedTurret = standardMachineGun;
        }
    }


    public void SelectMissileLauncher()
    {
        if (selectedTurret == missileLauncher)
        {
            Debug.Log("Deselected Missile Launcher");
            Manager.instance.DeselectTurretToBuild();
            selectedTurret = null;
        }
        else
        {
            Debug.Log("Selected Missile Launcher");
            Manager.instance.SelectTurretToBuild(missileLauncher);
            selectedTurret = missileLauncher;
        }
    }

    public void SelectLaserGun()
    {
        if (selectedTurret == laserGun)
        {
            Debug.Log("Deselected Laser Gun");
            Manager.instance.DeselectTurretToBuild();
            selectedTurret = null;
        }
        else
        {
            Debug.Log("Selected Laser gun");
            Manager.instance.SelectTurretToBuild(laserGun);
            selectedTurret = laserGun;
        }
    }
}
