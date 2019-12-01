using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public InGameMenu menu;
    public Slider slider;

    public float maxHealth = 0;
    public float health;

    public float gameOverTresholdRatio = 0.8f;
    private float baseHealth;

    public List<Burnable> props = new List<Burnable>();

    private void Start()
    {
        foreach (Burnable prop in props)
        {
            if (!prop.isInvincible)
            {
                maxHealth += prop.maxHealth;

            }
        }

        baseHealth = maxHealth * gameOverTresholdRatio;

        slider.maxValue = maxHealth - baseHealth;
        slider.value = maxHealth - baseHealth;
    }

    private void FixedUpdate()
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
            menu.Win();
        }

        slider.value = health - baseHealth;

        if (slider.value <= 0)
        {
            menu.GameOver();
        }
    }
}