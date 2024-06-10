using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    public Shop shop;
    private Manager manager;
    private Turret selectedTurret;

    private void Start()
    {
        manager = Manager.instance;
    }

    private void Update()
    {
        if (GameController.startPointPlaced && GameController.endPointPlaced)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Input.GetTouch(0).position;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Turret hitTurret = hit.transform.GetComponent<Turret>();
                    if (hitTurret != null)
                    {
                        // Show the NodeUI when a turret is clicked
                        Manager.instance.ShowNodeUI(hitTurret);
                    }
                    else
                    {
                        PlaceTurret(hit.point);
                    }
                }
                else
                {
                    Debug.LogError("No collider hit when trying to place turret!");
                }
            }
        }
    }


    private void PlaceTurret(Vector3 position)
    {
        if (shop.selectedTurret == null)
        {
            Debug.LogError("No turret selected to build!");
            return;
        }

        if (PlayerStats.money < shop.selectedTurret.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.money -= shop.selectedTurret.cost;
        Instantiate(shop.selectedTurret.prefab, position, Quaternion.identity);
        Debug.Log("Turret built! Money left: " + PlayerStats.money);
    }

    private void UpgradeTurret(Turret turret)
    {
        if (turret == null)
        {
            Debug.LogError("Turret is null!");
            return;
        }

        // Show the turret UI canvas
        turret.ShowTurretUI();
    }

}
