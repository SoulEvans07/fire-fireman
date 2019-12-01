using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public InGameMenu menu;
    public Slider healthSlider;
    public Slider timerSlider;

    public float maxHealth = 0;
    public float health;

    public float gameOverTresholdRatio = 0.8f;
    public float waitingTime = 10f;
    private float timer;
    private float baseHealth;

    public GameObject furnitureParent;
    public Burnable[] props;

    private void Start()
    {
        props = furnitureParent.GetComponentsInChildren<Burnable>();
        foreach (Burnable prop in props)
        {
            if (!prop.isInvincible)
            {
                maxHealth += prop.maxHealth;
            }
        }

        baseHealth = maxHealth * gameOverTresholdRatio;

        healthSlider.maxValue = maxHealth - baseHealth;
        healthSlider.value = maxHealth - baseHealth;

        timerSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        health = 0;
        bool isBurning = false;
        foreach (Burnable prop in props)
        {
            if (!prop.isInvincible)
            {
                health += prop.health;
                isBurning = isBurning || prop.isBurning;
            }
        }

        if (!isBurning)
        {
            timerSlider.gameObject.SetActive(true);
            if (timer < waitingTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                menu.Win();
            }
        }
        else
        {
            timerSlider.gameObject.SetActive(false);
            timer = 0;
        }

        timerSlider.value = (waitingTime - timer) / waitingTime;

        healthSlider.value = health - baseHealth;
        if (healthSlider.value <= 0)
        {
            menu.GameOver();
        }
    }
}