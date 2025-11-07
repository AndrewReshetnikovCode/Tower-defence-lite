using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Padding
{
    public float top;
    public float bottom;
    public float right;
    public float left;
}
/// <summary>
/// Camera moving and autoscaling.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
	// Camera speed when moving (dragging)
    public float dragSpeed = 2f;

	public float minSize = 1;
    public float maxSize = 20;
	public float zoomSpeed = 1;

    Camera _cam;

	bool _isDragging = false;
	Vector3 _prevMousePos;

    [Header("Bounds")]
    public SpriteRenderer boundarySprite;
    [SerializeField] Padding _padding;

    Vector2 minBounds;
    Vector2 maxBounds;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
	{
		_cam = GetComponent<Camera>();
        if (boundarySprite != null)
        {
            Bounds b = boundarySprite.bounds;
            minBounds = b.min;
            maxBounds = b.max;
        }
        Debug.Assert(boundarySprite && _cam, "Wrong initial settings");
	}

	/// <summary>
	/// Lates update this instance.
	/// </summary>
    void LateUpdate()
    {
		HandleZoom();

		if (Input.GetMouseButtonDown(0))
		{
			_prevMousePos = Input.mousePosition;
			_isDragging = true;
		}
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

		if (_isDragging)
		{
			Vector3 currPos = Input.mousePosition;
			Vector3 delta = _prevMousePos - currPos;
			_prevMousePos = currPos;

			Vector3 deltaNorm = _cam.ScreenToViewportPoint(delta);

			transform.Translate(deltaNorm * dragSpeed, Space.World);
		}
        ClampToBounds();
    }
	void HandleZoom()
	{
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		scroll *= zoomSpeed;
        scroll *= -1;

		float newSize = Mathf.Clamp(_cam.orthographicSize + scroll, minSize, maxSize);

		_cam.orthographicSize = newSize;
	}
    void ClampToBounds()
    {
        if (boundarySprite == null)
            return;

        float vertExtent = _cam.orthographicSize;
        float horzExtent = vertExtent * _cam.aspect;

        float leftBound = minBounds.x + horzExtent + _padding.left;
        float rightBound = maxBounds.x - horzExtent - _padding.right;
        float bottomBound = minBounds.y + vertExtent + _padding.bottom;
        float topBound = maxBounds.y - vertExtent - _padding.top;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        transform.position = pos;
    }
}
