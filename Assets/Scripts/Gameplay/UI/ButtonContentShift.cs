using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ButtonContentShift : MonoBehaviour
{
    [SerializeField] Button _btn;
    [SerializeField] private Vector2 pixelOffset = new Vector2(0, -5f);
    private RectTransform rectTransform;
    private Vector2 originalAnchoredPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalAnchoredPosition = rectTransform.anchoredPosition;
    }

    /// <summary>
    /// Смещает UI-элемент на заданное количество пикселей (в экранных координатах Canvas).
    /// </summary>
    public void Shift()
    {
        if (_btn.interactable == false)
            return;

        rectTransform.anchoredPosition = originalAnchoredPosition + pixelOffset;
    }

    /// <summary>
    /// Возвращает UI-элемент в исходное положение.
    /// </summary>
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalAnchoredPosition;
    }
}
