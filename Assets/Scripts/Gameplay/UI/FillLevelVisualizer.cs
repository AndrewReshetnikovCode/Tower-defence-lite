using UnityEngine;

[ExecuteAlways]
public class FillLevelVisualizer : MonoBehaviour
{
    [Header("Контейнер с вариантами визуала (дети)")]
    [SerializeField] private Transform container;

    [Header("Тестовые значения (для отладки в редакторе)")]
    [Range(0f, 1f)][SerializeField] private float debugFill = 0f;
    [SerializeField] private bool liveUpdateInEditor = true;

    [Header("Настройки смещения")]
    [Tooltip("Смещение соответствия (0 = без смещения, 1 = середина -> максимум).")]
    [Range(0f, 1f)][SerializeField] private float offset = 0f;

    private GameObject[] variants;

    private void Awake()
    {
        CacheVariants();
        UpdateVisualByFill(debugFill);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying && liveUpdateInEditor)
        {
            CacheVariants();
            UpdateVisualByFill(debugFill);
        }
    }

    private void CacheVariants()
    {
        if (container == null) return;

        int count = container.childCount;
        variants = new GameObject[count];
        for (int i = 0; i < count; i++)
            variants[i] = container.GetChild(i).gameObject;
    }

    /// <summary>
    /// Обновляет визуал в зависимости от заполнения current/max.
    /// </summary>
    public void UpdateVisual(float current, float max)
    {
        if (max <= 0f) return;
        float fill = Mathf.Clamp01(current / max);
        UpdateVisualByFill(fill);
    }

    private void UpdateVisualByFill(float fill)
    {
        if (variants == null || variants.Length == 0)
            return;

        // Применяем смещение
        fill = Mathf.Clamp01(fill + offset - 0.5f * offset);

        int index = Mathf.RoundToInt(fill * (variants.Length - 1));

        for (int i = 0; i < variants.Length; i++)
            variants[i].SetActive(i == index);
    }
}
