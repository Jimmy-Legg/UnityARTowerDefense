using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one BuildManager in scene!");
        instance = this;
    }

    [Header("Effect")]
    public GameObject buildEffect;

    private TurretBlueprint TurretToBuild;

    public bool CanBuild { get { return TurretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= TurretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        if (turret != null)
        {
            TurretToBuild = turret;
            Debug.Log("Selected turret to build: " + TurretToBuild.prefab.name);
        }
        else
        {
            Debug.LogError("Cannot select a null turret blueprint!");
        }
    }


    public void DeselectTurretToBuild()
    {
        TurretToBuild = null;
    }

    public void BuildTurretOn()
    {
        Debug.Log("Building turret: " + (TurretToBuild != null ? TurretToBuild.prefab.name : "TurretToBuild is null"));
        if (TurretToBuild == null)
        {
            Debug.LogError("No turret selected to build!");
            return;
        }

        if (PlayerStats.money < TurretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        if (TurretToBuild.prefab == null)
        {
            Debug.LogError("Prefab for selected turret is null!");
            return;
        }

        PlayerStats.money -= TurretToBuild.cost;

        Vector3 touchPosition = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(TurretToBuild.prefab + "Prefab");
            Instantiate(TurretToBuild.prefab, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No collider hit when trying to place turret!");
        }

        Debug.Log("Turret built! Money left: " + PlayerStats.money);
    }

    public Vector3 GetPreviewOffset()
    {
        if (TurretToBuild == null)
            return Vector3.zero;

        // Return the position offset of the selected turret blueprint
        return TurretToBuild.positionOffset;
    }
}
