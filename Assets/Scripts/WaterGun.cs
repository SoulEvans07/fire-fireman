using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public string SHOOT_KEY = "Fire1";
    private const string ANIMATOR_STATE = "state";
    private const float SHIFT_ANGLE = 22.5f;
    private const float L_ANGLE = 45f;

    private Transform _transform;
    public Transform waistPoint;
    public Animator _animator;
    private Vector3 crosshairMovement;
    public Camera cam;
    public LayerMask shootableMask;

    public Transform cursor;
    private bool pressed = false;

    public Vector2 dir;
    public float angle;

    private void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 target = cam.ScreenToWorldPoint(mousePos);
        target.z = 0;
        cursor.position = target;

        dir = (target - waistPoint.position).normalized;
        angle = Vector2.Angle(Vector2.up, dir);
        if (dir.x < 0) angle = 360 - angle;

        if ((L_ANGLE * 7.5 < angle && angle <= 360) || (0 < angle && angle <= 0.5f * L_ANGLE)) {
            _animator.SetInteger(ANIMATOR_STATE, 0);
        } else if (0.5f * L_ANGLE < angle && angle <= 1.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 1);
        } else if (1.5f * L_ANGLE < angle && angle <= 2.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 2);
        } else if (2.5f * L_ANGLE < angle && angle <= 3.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 3);
        } else if (3.5f * L_ANGLE < angle && angle <= 4.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 4);
        } else if (4.5f * L_ANGLE < angle && angle <= 5.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 5);
        } else if (5.5f * L_ANGLE < angle && angle <= 6.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 6);
        } else if (6.5f * L_ANGLE < angle && angle <= 7.5f * L_ANGLE) {
            _animator.SetInteger(ANIMATOR_STATE, 7);
        }

        if (Input.GetButtonDown(SHOOT_KEY) || pressed)
        {
            pressed = true;
        }

        if (Input.GetButtonUp(SHOOT_KEY))
        {
            pressed = false;
        }
    }
}
