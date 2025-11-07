using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridPreview))]
public class GridPreviewEditor : Editor
{
    void OnSceneGUI()
    {
        GridPreview preview = (GridPreview)target;
        if (preview.grid == null || !preview.show)
            return;

        Handles.color = preview.color;

        Vector3 cellSize = preview.grid.cellSize;
        Vector3 origin = preview.grid.transform.position;

        for (int x = 0; x <= preview.sizeX; x++)
        {
            Vector3 start = origin + new Vector3(x * cellSize.x, 0, 0);
            Vector3 end = origin + new Vector3(x * cellSize.x, preview.sizeY * cellSize.y, 0);
            Handles.DrawLine(start, end);
        }

        for (int y = 0; y <= preview.sizeY; y++)
        {
            Vector3 start = origin + new Vector3(0, y * cellSize.y, 0);
            Vector3 end = origin + new Vector3(preview.sizeX * cellSize.x, y * cellSize.y, 0);
            Handles.DrawLine(start, end);
        }
    }
}