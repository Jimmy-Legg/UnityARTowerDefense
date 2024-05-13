using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager)),
    RequireComponent(typeof(ARPlaneManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    public GameController gameController;

    [SerializeField]
    private GameObject startPrefab;

    [SerializeField]
    private GameObject endPrefab;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject currentPrefab;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        if (!GameController.startPointPlaced)
        {
            PlacePrefab(startPrefab);
            GameController.startPointPlaced = true;
        }
        else if (!GameController.endPointPlaced) 
        {
            PlacePrefab(endPrefab);
            GameController.endPointPlaced = true;
        }
    }


    private void PlacePrefab(GameObject prefab)
    {
        if (raycastManager.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                Pose pose = hit.pose;
                Instantiate(prefab, pose.position, pose.rotation);
                currentPrefab = prefab;
                return;
            }
        }
    }
}
