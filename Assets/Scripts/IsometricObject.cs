using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class IsometricObject : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 10;

    private Transform _transform;
    private Renderer _renderer;

    [Tooltip("Will use this object to compute z-order")]
    public Transform target;
    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int TargetOffset = 0;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (target == null)
            target = _transform;

        _renderer.sortingOrder = -(int)(target.position.y * IsometricRangePerYUnit) + TargetOffset;
    }
}