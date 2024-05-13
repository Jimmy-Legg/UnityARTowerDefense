using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color problemColor;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    private Manager manager;

    private GameObject previewPrefab;

    private float lastTapTime;
    private const float doubleTapTimeThreshold = 0.2f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        manager = Manager.instance;
    }

    public Vector3 GetBuildPosition(Vector3 offset)
    {
        return transform.position + offset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!manager.CanBuild)
            return;
        if (turret != null)
        {
            Debug.Log("Can't Build - TODO display on screen");
            return;
        }

        float timeSinceLastTap = Time.time - lastTapTime;
        if (timeSinceLastTap < doubleTapTimeThreshold)
        {
            // Double tap detected
            BuildTurret();
        }
        else
        {
            lastTapTime = Time.time;
        }
    }

    private void BuildTurret()
    {
        if (previewPrefab != null)
        {
            Destroy(GameObject.Find(previewPrefab.name + "(Clone)"));
        }

    }
}
