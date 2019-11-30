using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float tan30 = 0.57735026918f;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    public float speedRatio;
    public float speed;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Move(x, y);
    }

    private void Move(float x, float y) {
        _rigidbody.velocity = new Vector2(
            x * speed,
            y * speed * tan30
        );
    }
}
