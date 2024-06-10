using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool startPointPlaced = false;
    public static bool endPointPlaced = false;

    private void Awake()
    {
        startPointPlaced = false;
        endPointPlaced = false;
    }
    private void Start()
    {
        startPointPlaced = false;
        endPointPlaced = false;
    }
}
