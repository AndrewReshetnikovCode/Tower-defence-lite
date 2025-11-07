using UnityEngine;
using UnityEngine.Events;



public class RaycastEventReceiver2D : MonoBehaviour, IRaycastReceiver2D
{
    public RaycastEvent onPointerEnter;
    public RaycastEvent onPointerExit;
    public RaycastEvent onPointerStay;
    public RaycastEvent onPointerDown;
    public RaycastEvent onPointerUp;


    public void OnPointerEnter() => onPointerEnter?.Invoke();
    public void OnPointerExit() => onPointerExit?.Invoke();
    public void OnPointerStay() => onPointerStay?.Invoke();
    public void OnPointerDown() => onPointerDown?.Invoke();
    public void OnPointerUp() => onPointerUp?.Invoke();
}