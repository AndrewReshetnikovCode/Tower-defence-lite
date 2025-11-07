using System;
using UnityEngine;


public class CursorMagnet : MonoBehaviour
{
    [Serializable]
    public class Entry
    {
        public GameObject prefab;
        public Vector3 offset;
    }
    [Header("Настройки")]
    public Camera targetCamera;                // Камера, относительно которой рассчитывается позиция
    public Entry[] entries;               // Список префабов
    public float distanceToCamera = 2f;          // Расстояние от камеры до объекта

    private GameObject currentInstance;
    private bool isMagnetActive = false;
    int _currentSelection = -1;

    void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        if (isMagnetActive && currentInstance != null && _currentSelection != -1)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distanceToCamera;
            Vector3 worldPos = targetCamera.ScreenToWorldPoint(mousePos);
            currentInstance.transform.position = worldPos + entries[_currentSelection].offset;
        }
    }

    /// <summary>
    /// Создаёт объект из списка по индексу и магнитит к курсору.
    /// </summary>
    public void MagnetToCursor(int prefabIndex)
    {
        if (prefabIndex < 0 || prefabIndex >= entries.Length || entries[prefabIndex].prefab == null)
        {
            Debug.LogWarning("Неверный индекс префаба или отсутствует префаб.");
            return;
        }

        _currentSelection = prefabIndex;

        if (currentInstance != null)
            Destroy(currentInstance);

        currentInstance = Instantiate(entries[prefabIndex].prefab);
        isMagnetActive = true;
    }

    /// <summary>
    /// Останавливает магнит и удаляет текущий объект.
    /// </summary>
    public void StopMagnet()
    {
        if (currentInstance != null)
            Destroy(currentInstance);

        currentInstance = null;
        isMagnetActive = false;
        _currentSelection = -1;
    }
}
