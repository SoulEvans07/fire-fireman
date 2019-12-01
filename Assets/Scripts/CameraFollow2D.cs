using UnityEngine;

public class CameraFollow2D : MonoBehaviour {
   
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform cam;
    public Transform target;
   
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (target)
        {
            Vector3 delta = target.position - cam.position;
            Vector3 destination = transform.position + delta;
            destination.z = cam.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
       
    }
}