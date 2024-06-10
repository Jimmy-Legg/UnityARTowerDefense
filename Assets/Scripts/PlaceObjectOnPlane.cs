using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager)), RequireComponent(typeof(ARPlaneManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    // Référence au contrôleur de jeu
    public GameController gameController;

    // Préfabriqués pour les objets de départ et de fin
    [SerializeField]
    private GameObject startPrefab;

    [SerializeField]
    private GameObject endPrefab;

    // Gestionnaires AR pour le raycasting et la gestion des plans
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

    // Liste pour stocker les résultats des hits de raycasting
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Instances des objets de départ et de fin
    private GameObject startInstance;
    private GameObject endInstance;

    // Initialisation des gestionnaires AR
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Activation des simulations et support de touches améliorées, ajout du gestionnaire d'événements
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    // Désactivation des simulations et support de touches améliorées, suppression du gestionnaire d'événements
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    // Démarrage, s'assure que les points de départ et de fin ne sont pas placés initialement
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

    // Gestionnaire d'événements
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        // Ignore si ce n'est pas le premier doigt
        if (finger.index != 0) return;

        // Vérifie si la touche est sur un plan détecté
        if (raycastManager.Raycast(finger.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Place le point de départ s'il n'est pas encore placé
            if (!GameController.startPointPlaced)
            {
                PlacePrefab(startPrefab, ref startInstance);
                GameController.startPointPlaced = true;
            }
            // Place le point de fin s'il n'est pas encore placé
            else if (!GameController.endPointPlaced)
            {
                PlacePrefab(endPrefab, ref endInstance);
                GameController.endPointPlaced = true;
            }
        }
    }

    // Méthode pour placer un préfabriqué sur le premier hit de raycasting
    private void PlacePrefab(GameObject prefab, ref GameObject instance)
    {
        foreach (ARRaycastHit hit in hits)
        {
            Pose pose = hit.pose;
            // Supprime l'instance précédente si elle existe
            if (instance != null)
            {
                Destroy(instance);
            }
            // Instancie le nouveau préfabriqué à la position du hit
            instance = Instantiate(prefab, pose.position, pose.rotation);
            return;
        }
    }
}
