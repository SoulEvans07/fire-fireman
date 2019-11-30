using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float tan30 = 0.57735026918f;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = tan30 * Input.GetAxisRaw("Vertical");

        gameObject.transform.position = new Vector2 (
            transform.position.x + (h * speed), 
            transform.position.y + (v * speed));
    }
}
