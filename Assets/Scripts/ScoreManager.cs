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

    public GameObject matchBox;
    private Image[] matches;
    public Sprite goodMatch;
    public Sprite burntMatch;

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

        matches = matchBox.GetComponentsInChildren<Image>();
        foreach (Image match in matches)
        {
            match.sprite = goodMatch;
        }
    }

    private void Update()
    {
        health = 0;
        bool isBurning = false;
        int burntCount = 0;
        foreach (Burnable prop in props)
        {
            if (!prop.isInvincible)
            {
                health += prop.health;
                isBurning = isBurning || prop.isBurning;
                if (prop.health == 0) {
                    burntCount++;
                }
            }
        }

        for (int i = 0; i < Mathf.Min(burntCount, matches.Length); i++) {
            matches[matches.Length - 1 - i].sprite = burntMatch;
        }

        if (burntCount >= matches.Length) {
            menu.GameOver();
            return;
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
                return;
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
            return;
        }
    }
}