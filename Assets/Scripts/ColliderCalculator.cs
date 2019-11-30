using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderCalculator : MonoBehaviour
{
    private const float tan30 = 0.57735026918f;

    private PolygonCollider2D _collider;

    public Transform pointLeft;
    public Transform pointRight;

    private Vector2 positionLeft;
    private Vector2 positionRight;

    private Vector2 positionUp;
    private Vector2 positionDown;

    void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();

        CalcCollider();
    }

    private void CalcCollider() {
        positionLeft = pointLeft.localPosition;
        positionRight = pointRight.localPosition;

        float width = positionRight.x - positionLeft.x;
        float height = width * tan30;

        float heightSmall = Mathf.Abs(positionRight.y - positionLeft.y);
        float widthSmall = heightSmall / tan30;

        float cosinusSmall = (width - widthSmall) / 2;
        float sinusSmall = cosinusSmall * tan30;

        float baseY = Mathf.Min(positionLeft.y, positionRight.y) - sinusSmall;
        float topY = Mathf.Max(positionLeft.y, positionRight.y) + sinusSmall;

        if (positionLeft.y > positionRight.y)
        {
            positionUp = new Vector2(positionLeft.x + cosinusSmall, topY);
            positionDown = new Vector2(positionRight.x - cosinusSmall, baseY);
        }
        else
        {
            positionUp = new Vector2(positionRight.x - cosinusSmall, topY);
            positionDown = new Vector2(positionLeft.x + cosinusSmall, baseY);
        }

        _collider.SetPath(0, new[] { positionLeft, positionUp, positionRight, positionDown });
    }

    void Update()
    {
        CalcCollider();
    }
}
