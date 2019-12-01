using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public string SHOOT_KEY = "Fire1";

    private PlayerAnimation animController;
    
    private Transform _transform;
    public List<GameObject> waterStreams;
    public GameObject water;
    
    private void Awake()
    {
        _transform = transform;
        animController = GetComponent<PlayerAnimation>();

        foreach (GameObject ws in waterStreams)
        {
            ws.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
           
        int state = animController.state;
        int prevState = animController.prevState;


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
