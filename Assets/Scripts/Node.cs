using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;

    public Color startColor;

    Manager manager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        manager = Manager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (manager.GetMachineGunToBuild() == null)
            return;
        if(turret != null)
        {
            Debug.Log("Cant Build - TODO display on screen");
            return;
        }

        GameObject machineGunTobuild = Manager.instance.GetMachineGunToBuild();

        turret = (GameObject)Instantiate(machineGunTobuild, transform.position + positionOffset, transform.rotation);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (manager.GetMachineGunToBuild() == null)
            return;
        rend.material.color = hoverColor ;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
