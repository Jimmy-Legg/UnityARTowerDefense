using UnityEngine;
using UnityEngine.UI;

public class HideCanvasWhenPointsPlaced : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField]
    Canvas canvas;


    private void Update()
    {
        if (GameController.startPointPlaced && GameController.endPointPlaced)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
