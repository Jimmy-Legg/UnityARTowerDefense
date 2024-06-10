using UnityEngine;
using UnityEngine.UI;
using static DataManager;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Turret target;

    public Button upgradeButton;

    [SerializeField]
    private Color ActiatedColor;
    [SerializeField]
    private Color DisabledColor;

    public void SetTarget(Turret _target)
    {
        target = _target;

        // Set UI position
        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = target.GetBuildPosition(offset);

        ui.SetActive(true);

        if (upgradeButton != null)
        {
            // Check upgrade conditions
            if (!target.CanUpgrade)
            {
                DisableButton();
                return;
            }

            upgradeButton.enabled = true;
            upgradeButton.GetComponent<Image>().color = ActiatedColor;
        }
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret(); 
    }

    public void Sell()
    {
        return;
    }

    public void DisableButton()
    {
        upgradeButton.enabled = false;
        upgradeButton.GetComponent<Image>().color = DisabledColor;
    }
}
