using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIElementFitController : MonoBehaviour
{
    RectTransform _uiElement;

    void Start()
    {
        _uiElement = GetComponent<RectTransform>();
    }

    public void SetScreenPosition(Vector3 screenPos)
    {
        if (_uiElement == null)
            return;

        // Получаем размеры элемента в пикселях
        Vector2 size = _uiElement.rect.size;
        Vector2 pivot = _uiElement.pivot;

        // Рассчитываем половины с учетом pivot
        float leftOffset = size.x * pivot.x;
        float rightOffset = size.x * (1f - pivot.x);
        float bottomOffset = size.y * pivot.y;
        float topOffset = size.y * (1f - pivot.y);

        // Ограничиваем координаты
        float x = Mathf.Clamp(screenPos.x, leftOffset, Screen.width - rightOffset);
        float y = Mathf.Clamp(screenPos.y, bottomOffset, Screen.height - topOffset);

        // Присваиваем позицию
        _uiElement.position = new Vector3(x, y, screenPos.z);

    }
}
