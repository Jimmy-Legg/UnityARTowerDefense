using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    public Shop shop;

    private Manager manager;

    private void Update()
    {
        if (GameController.startPointPlaced && GameController.endPointPlaced)
        {
            Debug.Log("Start and end points placed!");
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (PlayerStats.money < shop.selectedTurret.cost)
                {
                    Debug.Log("Not enough money to build that!");
                    return;
                }
                if (shop.selectedTurret == null)
                {
                    Debug.LogError("No turret selected to build!");
                    return;
                }
                if (shop.selectedTurret.prefab == null)
                {
                    Debug.LogError("Prefab for selected turret is null!");
                    return;
                }
                PlayerStats.money -= shop.selectedTurret.cost;

                Vector3 touchPosition = Input.GetTouch(0).position;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(shop.selectedTurret.prefab + "Prefab");
                    Instantiate(shop.selectedTurret.prefab, hit.point, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("No collider hit when trying to place turret!");
                }

                Debug.Log("Turret built! Money left: " + PlayerStats.money);
            }
        }
    }
}
