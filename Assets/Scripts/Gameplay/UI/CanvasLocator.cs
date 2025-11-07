using UnityEngine;


public class CanvasLocator : MonoBehaviour
{
    static CanvasLocator instance;

    public Canvas mainCanvas;

    public static Canvas MainCanvas => instance.mainCanvas;
}
