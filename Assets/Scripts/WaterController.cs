using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private Transform _transform;

    public Vector3 target;
    public float speed;
 
    private void Awake()
    {
        _transform = transform;
    }


    private void Update()
    {
       _transform.position += _transform.up * speed;
    }
}
