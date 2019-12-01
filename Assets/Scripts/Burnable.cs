using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public bool isFlammable = true;
    public bool canSpreadFireToOthers = true;

    public float maxHealth;
    public float health = 100f;
    public float temperature = 0;
    public bool isBurning = false;

    public float heatResistance = 1;
    public float burnIntensity = 1;

    public bool isInvincible = false;

    public float inflammationTreshold = 90f;
    public float extinguishTreshold = 70f;
    public float minimumTemperature = -50f;

    public ParticleSystem firePS;

    public List<ParticleSystem> extraFirePS;

    public Collider2D fireCollider;
    public GameObject pointLight;

    private float tickInterval = 0.1f;

    private float updateTimer = 0f;

    private SpriteRenderer spriteRenderer;

    private Color baseColor;
    public Color wetColor;
    public Color heatColor;
    public Color burntColor;
    private Color targetColor;

    private Dictionary<GameObject, int> collidedBurnables = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> collidedWaters = new Dictionary<GameObject, int>();

    void Awake()
    {
        maxHealth = health;
        baseColor = Color.white;

        if (isBurning)
        {
            StartFire();
        }
        else
        {
            StopFire();
        }

        fireCollider.isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != this.gameObject)
        {
            if (other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Player"))
            {
                AddToDictionary(collidedBurnables, other.gameObject);
            }

            if (other.gameObject.CompareTag("Water"))
            {
                AddToDictionary(collidedWaters, other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != this.gameObject)
        {
            if (other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Player"))
            {
                RemoveFromDictionary(collidedBurnables, other.gameObject);
            }

            if (other.gameObject.CompareTag("Water"))
            {
                RemoveFromDictionary(collidedWaters, other.gameObject);
            }
        }
    }

    void OnTick()
    {
        if (health > 0 && !isInvincible && isBurning)
        {
            health--;

            if (health <= 0 && this.isBurning)
            {
                StopFire();

                spriteRenderer.color = burntColor;
            }
        }
    }

    void OnCollisionTick(Burnable otherObject)
    {
        if (otherObject.isBurning && otherObject.canSpreadFireToOthers)
        {
            this.temperature = Mathf.Min(inflammationTreshold, this.temperature + otherObject.burnIntensity / this.heatResistance);

            if (this.health > 0 && this.isFlammable)
            {
                if (!this.isBurning && this.temperature >= this.inflammationTreshold)
                {
                    StartFire();
                }
            }
        }
    }

    void OnCollisionTick(Water otherObject)
    {
        this.temperature = Mathf.Max(minimumTemperature, this.temperature - otherObject.intensity);

        if (this.isBurning && this.temperature <= this.extinguishTreshold)
        {
            StopFire();
        }
    }

    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer > tickInterval)
        {
            updateTimer = updateTimer - tickInterval;
            OnTick();

            foreach (GameObject burnable in GetItemsFromDictionary(collidedBurnables))
            {
                OnCollisionTick(burnable.GetComponent<Burnable>());
            }

            foreach (GameObject water in GetItemsFromDictionary(collidedWaters))
            {
                OnCollisionTick(water.GetComponent<Water>());
            }
        }

        UpdateColor();
    }

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }

    private void UpdateColor()
    {
        if (isInvincible) return;

        if (isBurning || health == 0)
        {
            baseColor = Color.Lerp(Color.white, burntColor, (maxHealth - health) / maxHealth);
            targetColor = burntColor;
            spriteRenderer.color = baseColor;
        }
        else if (temperature < 0)
        {
            targetColor = CombineColors(baseColor, wetColor);
            spriteRenderer.color = Color.Lerp(baseColor, targetColor, temperature / minimumTemperature);
        }
        else
        {
            targetColor = CombineColors(baseColor, heatColor);
            spriteRenderer.color = Color.Lerp(baseColor, targetColor, temperature / inflammationTreshold);
        }
    }

    private void StartFire()
    {
        if (extraFirePS != null)
        {
            foreach (ParticleSystem ps in extraFirePS)
            {
                ps.Play();
            }
        }

        firePS.Play();
        pointLight.SetActive(true);
        this.isBurning = true;
    }

    private void StopFire()
    {
        if (extraFirePS != null)
        {
            foreach (ParticleSystem ps in extraFirePS)
            {
                ps.Stop();
            }
        }

        firePS.Stop();
        pointLight.SetActive(false);
        this.isBurning = false;
    }

    #region DictionaryHelpers

    private static void AddToDictionary<T>(Dictionary<T, int> dict, T item)
    {
        if (dict.ContainsKey(item))
        {
            dict[item]++;
        }
        else
        {
            dict.Add(item, 1);
        }
    }

    private static void RemoveFromDictionary<T>(Dictionary<T, int> dict, T item)
    {
        dict[item]--;
    }

    private static IEnumerable<T> GetItemsFromDictionary<T>(Dictionary<T, int> dict)
    {
        foreach (KeyValuePair<T, int> tuple in dict)
        {
            if (tuple.Value > 0)
            {
                yield return tuple.Key;
            }
        }
    }

    #endregion DictionaryHelpers
}
