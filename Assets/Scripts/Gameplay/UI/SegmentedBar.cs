using System.Collections.Generic;
using UnityEngine;

public class SegmentedBar : MonoBehaviour
{
    [SerializeField] Transform _segmentsContainer;
    List<GameObject> _segments = new();

    [Tooltip("Если true – округляется вниз, если false – по правилам математики")]
    [SerializeField] bool _floorRounding = true;

    int _totalSegments;

    private void Awake()
    {
        _totalSegments = _segmentsContainer.childCount;

        for (int i = 0; i < _segmentsContainer.childCount; i++)
        {
            _segments.Add(_segmentsContainer.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Обновление отображения шкалы.
    /// value – нормализованное значение [0..1].
    /// </summary>
    public void UpdateBar(float value)
    {
        value = Mathf.Clamp01(value);
        float segmentsToShow = value * _totalSegments;

        int activeSegments = _floorRounding
            ? Mathf.FloorToInt(segmentsToShow)
            : Mathf.RoundToInt(segmentsToShow);

        for (int i = 0; i < _totalSegments; i++)
        {
            _segments[i].SetActive(i < activeSegments);
        }
    }
}
