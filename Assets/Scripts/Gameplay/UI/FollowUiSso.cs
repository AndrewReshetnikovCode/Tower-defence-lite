using UnityEngine;

[ExecuteAlways]
public class FollowUiSso : MonoBehaviour
{
    [Header("UI объект (RectTransform на Canvas Overlay)")]
    public RectTransform uiTarget;

    [Header("Камера, в пространстве которой должен находиться объект")]
    public Camera targetCamera;

    [Header("Глубина (расстояние от камеры)")]
    public float distanceFromCamera = 5f;

    private void LateUpdate()
    {
        if (uiTarget == null || targetCamera == null)
            return;

        // Получаем экранную позицию UI-элемента
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiTarget.position);

        // Конвертируем экранную позицию в мировую координату камеры
        Vector3 worldPos = targetCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));

        transform.position = worldPos;
    }
}
