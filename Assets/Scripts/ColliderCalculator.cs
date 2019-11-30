using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderCalculator : MonoBehaviour
{
    private const float tan30 = 0.57735026918f;

    public PolygonCollider2D hardCollider;
    public PolygonCollider2D fireCollider;

    public Transform pointLeft;
    public Transform pointRight;

    public float fireRange;

    private Vector2 positionLeft;
    private Vector2 positionRight;

    private Vector2 positionUp;
    private Vector2 positionDown;

    void Awake()
    {
        CalcColliders();
    }

    private void CalcColliders()
    {
        if (hardCollider || fireCollider) {
            CalcBoundingRect();
        }

        if (hardCollider) {
            CalcHardCollider();
        }

        if (fireCollider) {
            CalcFireCollider();
        }

    }

    private void CalcBoundingRect() 
    {
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
    }

    private void CalcHardCollider() 
    {
        hardCollider.SetPath(0, new[] { positionLeft, positionUp, positionRight, positionDown });
    }

    private void CalcFireCollider()
    {
        float rangeX = fireRange;
        float rangeY = fireRange * tan30;

        fireCollider.SetPath(0, new[] 
        { 
            new Vector2(positionLeft.x - rangeX, positionLeft.y - rangeY),
            new Vector2(positionLeft.x - rangeX, positionLeft.y + rangeY),
            new Vector2(positionUp.x - rangeX, positionUp.y + rangeY),
            new Vector2(positionUp.x + rangeX, positionUp.y + rangeY),
            new Vector2(positionRight.x + rangeX, positionRight.y + rangeY),
            new Vector2(positionRight.x + rangeX, positionRight.y - rangeY),
            new Vector2(positionDown.x + rangeX, positionDown.y - rangeY),
            new Vector2(positionDown.x - rangeX, positionDown.y - rangeY)
        });
    }

    void Update()
    {
        CalcColliders();
    }
}
