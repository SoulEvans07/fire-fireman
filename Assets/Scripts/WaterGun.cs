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

    

    public Vector2 dir;
    public float angle;
    public int state = 0;

    public Transform cursor;
    public List<GameObject> waterStreams;
    public GameObject water;

    private void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();

        foreach (GameObject ws in waterStreams)
        {
            ws.SetActive(false);
        }
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

        int prevState = state;
        state = (int) Mathf.Ceil(Mathf.Floor(angle / L_ANGLE / 0.5f) / 2);
        if(state == 8) {
            state = 0;
        }

        _animator.SetInteger(ANIMATOR_STATE, state);


        if (Input.GetButtonDown(SHOOT_KEY) || water != null)
        {
            water = waterStreams[state];
            if (prevState != state) {
                waterStreams[prevState].SetActive(false);
            }
            water.SetActive(true);
        }

        if (Input.GetButtonUp(SHOOT_KEY))
        {
            water.SetActive(false);
            water = null;
        }
    }
}
