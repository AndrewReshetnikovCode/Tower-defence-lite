using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GridPreview : MonoBehaviour
{
    public Grid grid; // выбранная сетка
    public Color color = new Color(0f, 1f, 0f, 0.5f);
    public int sizeX = 10;
    public int sizeY = 10;
    public bool show = true;
}

