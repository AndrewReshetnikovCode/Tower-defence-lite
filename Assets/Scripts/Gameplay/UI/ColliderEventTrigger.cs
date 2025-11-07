using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Collider2D))]
public class ColliderEventTrigger : MonoBehaviour
{

    [Header("Mouse Events")]
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;
    public UnityEvent onPointerDown;
    public UnityEvent onPointerUp = new ();
    public UnityEvent onPointerClick = new ();

    [Header("Collision Events")]
    public UnityEvent onTriggerEnter = new ();
    public UnityEvent onTriggerExit = new ();
    public UnityEvent onTriggerStay = new ();

    [Header("Drag Events")]
    public UnityEvent onBeginDrag = new ();
    public UnityEvent onDrag = new ();
    public UnityEvent onEndDrag = new ();

    private bool isDragging;
    private bool isMouseOver;
    private float dragThreshold = 0.1f;
    private Vector3 dragStartPosition;

    void OnMouseEnter()
    {
        isMouseOver = true;
        onPointerEnter.Invoke();
    }

    void OnMouseExit()
    {
        isMouseOver = false;
        onPointerExit.Invoke();
    }

    void OnMouseDown()
    {
        onPointerDown.Invoke();
        StartDragging();
    }

    void OnMouseUp()
    {
        onPointerUp.Invoke();

        if (isMouseOver && !isDragging)
        {
            onPointerClick.Invoke();
        }

        StopDragging();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            onDrag.Invoke();
        }
    }

    void OnTriggerEnter(Collider other) => onTriggerEnter.Invoke();
    void OnTriggerExit(Collider other) => onTriggerExit.Invoke();
    void OnTriggerStay(Collider other) => onTriggerStay.Invoke();

    private void StartDragging()
    {
        isDragging = false;
        dragStartPosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)
            && !isDragging
            && Vector3.Distance(dragStartPosition, Input.mousePosition) > dragThreshold)
        {
            isDragging = true;
            onBeginDrag.Invoke();
        }
    }

    private void StopDragging()
    {
        if (isDragging)
        {
            onEndDrag.Invoke();
        }
        isDragging = false;
    }
}