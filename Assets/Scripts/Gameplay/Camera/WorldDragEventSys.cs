using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WorldDragEventSys : MonoBehaviour
{
    bool _isDragging = false;
    Vector3 _lastMsPos;
    private void Update()
    {
        Vector2Int borderDir = GetMouseEdgeDirection();

        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> r = UIRaycastUtility.GetUIElementsUnderCursor(CanvasLocator.MainCanvas);
            if (r.Count == 0)
            {
                _isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector3 delta = Input.mousePosition - _lastMsPos;




            _lastMsPos = Input.mousePosition;
        }


        Vector2Int dir = GetMouseEdgeDirection();
        if (dir != Vector2Int.zero)
            Debug.Log($"Курсор у края: {dir}");
    }
    public int edgeThickness = 5;

    public Vector2Int GetMouseEdgeDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        int xDir = 0;
        int yDir = 0;

        if (mousePos.x <= edgeThickness)
            xDir = -1;
        else if (mousePos.x >= screenWidth - edgeThickness)
            xDir = 1;

        if (mousePos.y <= edgeThickness)
            yDir = -1;
        else if (mousePos.y >= screenHeight - edgeThickness)
            yDir = 1;

        return new Vector2Int(xDir, yDir);
    }

}
