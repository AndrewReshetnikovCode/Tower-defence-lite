using UnityEngine;
using UnityEngine.Events;

public class Raycaster2D : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private IRaycastReceiver2D currentReceiver;
    [SerializeField] LayerMask _layerMask;
    private void Update()
    {
        if (targetCamera == null) return;

        Ray r = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(r.origin, r.direction, _layerMask);
        Debug.Log(hit.transform);
        HandleRaycast(hit);
    }

    private void HandleRaycast(RaycastHit2D hit)
    {
        IRaycastReceiver2D newReceiver = hit.collider?.GetComponent<IRaycastReceiver2D>();

        // Если объект под курсором изменился
        if (currentReceiver != newReceiver)
        {
            currentReceiver?.OnPointerExit();
            currentReceiver = newReceiver;
            currentReceiver?.OnPointerEnter();
            return;
        }

        // Если курсор остается на том же объекте
        if (currentReceiver != null)
        {
            currentReceiver.OnPointerStay();
            if (Input.GetMouseButtonDown(0))
            {
                currentReceiver.OnPointerDown();
            }
            if (Input.GetMouseButtonUp(0))
            {
                currentReceiver.OnPointerUp();
            }
        }
    }

    private void OnDisable()
    {
        if (currentReceiver != null)
        {
            currentReceiver.OnPointerExit();
            currentReceiver = null;
        }
    }
}

public interface IRaycastReceiver2D
{
    void OnPointerEnter();
    void OnPointerExit();
    void OnPointerStay();
    void OnPointerDown();
    void OnPointerUp();
}

[System.Serializable]
public class RaycastEvent : UnityEvent { }