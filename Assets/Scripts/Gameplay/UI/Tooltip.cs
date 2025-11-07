using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [TextArea(3,20)] string _text;
    [SerializeField] Vector2 _offset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus.Publish(new TooltipEventArgs()
        {
            display = true,
            screenPosition = transform.position + (Vector3)_offset,
            text = _text
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventBus.Publish(new TooltipEventArgs()
        {
            display = false,
        });
    }
}