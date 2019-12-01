using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private const string ANIMATOR_STATE = "state";
    private const float L_ANGLE = 45f;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    public Transform waistPoint;
    private Animator _animator;

    private Vector3 crosshairMovement;
    public Camera cam;

    private Vector2 dir;
    private float angle;
    public int state = 0;
    public int prevState;
    private int animState;

    public Transform cursor;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 target = cam.ScreenToWorldPoint(mousePos);
        target.z = 0;
        cursor.position = target;

        dir = (target - waistPoint.position).normalized;
        angle = Vector2.Angle(Vector2.up, dir);
        if (dir.x < 0) angle = 360 - angle;

        prevState = state;
        state = (int) Mathf.Ceil(Mathf.Floor(angle / L_ANGLE / 0.5f) / 2);
        if(state == 8) {
            state = 0;
        }

        if (_rigidbody.velocity.magnitude > 0.001 && state % 2 == 1) {
            animState = state + 10;
        } else {
            animState = state;
        }

        _animator.SetInteger(ANIMATOR_STATE, animState);
    }
}