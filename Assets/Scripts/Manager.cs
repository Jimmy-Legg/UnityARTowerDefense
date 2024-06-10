using UnityEngine;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one Manager in scene!");
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

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Touch is over a UI element!");
            return;
        }

        Vector3 touchPosition = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the placement position overlaps with any existing turret's range area
            Turret[] turrets = FindObjectsOfType<Turret>();
            foreach (Turret turret in turrets)
            {
                if (Vector3.Distance(turret.transform.position, hit.point) < turret.range)
                {
                    Debug.LogError("Cannot place turret within the range of another turret!");
                    return;
                }
            }

            PlayerStats.money -= TurretToBuild.cost;

            Debug.Log(TurretToBuild.prefab + " Prefab");
            Instantiate(TurretToBuild.prefab, hit.point, Quaternion.identity);
            Debug.Log("Turret built! Money left: " + PlayerStats.money);
        }
        else
        {
            Debug.LogError("No collider hit when trying to place turret!");
        }
    }
    public void ShowNodeUI(Turret turret)
    {
        if (turret != null && turret.turretUI != null)
        {
            turret.turretUI.GetComponent<NodeUI>().SetTarget(turret);
        }
    }

    public void HideNodeUI(Turret turret)
    {
        if (turret != null && turret.turretUI != null)
        {
            turret.turretUI.GetComponent<NodeUI>().Hide();
        }
    }
    public Vector3 GetPreviewOffset()
    {
        if (TurretToBuild == null)
            return Vector3.zero;

        // Return the position offset of the selected turret blueprint
        return TurretToBuild.positionOffset;
    }
}
