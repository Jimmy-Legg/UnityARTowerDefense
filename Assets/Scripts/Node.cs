using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color problemColor;
    public Vector3 positionOffset;


    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;

    public Color startColor;

    Manager manager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        manager = Manager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!manager.CanBuild)
            return;
        if(turret != null)
        {
            Debug.Log("Cant Build - TODO display on screen");
            return;
        }

        manager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!manager.CanBuild)
            return;

        if (!manager.HasMoney)
        {
            rend.material.color = problemColor;
        }
        else if (turret != null)
        {
            rend.material.color = problemColor;
        }
        else
        {
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
