using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager)), RequireComponent(typeof(ARPlaneManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    // R�f�rence au contr�leur de jeu
    public GameController gameController;

    // Pr�fabriqu�s pour les objets de d�part et de fin
    [SerializeField]
    private GameObject startPrefab;

    [SerializeField]
    private GameObject endPrefab;

    // Gestionnaires AR pour le raycasting et la gestion des plans
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

    // Liste pour stocker les r�sultats des hits de raycasting
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Instances des objets de d�part et de fin
    private GameObject startInstance;
    private GameObject endInstance;

    // Initialisation des gestionnaires AR
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Activation des simulations et support de touches am�lior�es, ajout du gestionnaire d'�v�nements
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    // D�sactivation des simulations et support de touches am�lior�es, suppression du gestionnaire d'�v�nements
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    // D�marrage, s'assure que les points de d�part et de fin ne sont pas plac�s initialement
    private void Start()
    {
        if (startInstance != null)
        {
            Destroy(startInstance);
            GameController.startPointPlaced = false;
        }
        if (endInstance != null)
        {
            Destroy(endInstance);
            GameController.endPointPlaced = false;
        }
    }

    // Gestionnaire d'�v�nements
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        // Ignore si ce n'est pas le premier doigt
        if (finger.index != 0) return;

        // V�rifie si la touche est sur un plan d�tect�
        if (raycastManager.Raycast(finger.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Place le point de d�part s'il n'est pas encore plac�
            if (!GameController.startPointPlaced)
            {
                PlacePrefab(startPrefab, ref startInstance);
                GameController.startPointPlaced = true;
            }
            // Place le point de fin s'il n'est pas encore plac�
            else if (!GameController.endPointPlaced)
            {
                PlacePrefab(endPrefab, ref endInstance);
                GameController.endPointPlaced = true;
            }
        }
    }

    // M�thode pour placer un pr�fabriqu� sur le premier hit de raycasting
    private void PlacePrefab(GameObject prefab, ref GameObject instance)
    {
        foreach (ARRaycastHit hit in hits)
        {
            Pose pose = hit.pose;
            // Supprime l'instance pr�c�dente si elle existe
            if (instance != null)
            {
                Destroy(instance);
            }
            // Instancie le nouveau pr�fabriqu� � la position du hit
            instance = Instantiate(prefab, pose.position, pose.rotation);
            return;
        }
    }
}
