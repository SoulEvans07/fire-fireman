using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public bool isFlammable = true;
    public bool canSpreadFireToOthers = true;

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
    public Collider2D fireCollider;

    private float tickInterval = 0.1f;

    private float updateTimer = 0f;

    private SpriteRenderer spriteRenderer;

    public Color burntColor;

    private Dictionary<GameObject, int> collidedBurnables = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> collidedWaters = new Dictionary<GameObject, int>();

    void Awake()
    {
        if (isBurning)
        {
            firePS.Play();
        }
        else
        {
            firePS.Stop();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != this.gameObject && 
            (other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Player"))) 
        {
            RemoveFromDictionary(collidedBurnables, other.gameObject);
        }
    }

    void OnTick()
    {
        if (health > 0 && !isInvincible && isBurning)
        {
            health--;

            if (health <= 0 && this.isBurning)
            {
                this.isBurning = false;
                firePS.Stop();

                spriteRenderer.color = burntColor;
            }
        }
    }

    void OnCollisionTick(Burnable otherObject)
    {
        if (this.isFlammable && otherObject.isBurning && otherObject.canSpreadFireToOthers)
        {
            this.temperature = Mathf.Min(inflammationTreshold, this.temperature + otherObject.burnIntensity / this.heatResistance);

            if (this.health > 0)
            {
                if (!this.isBurning && this.temperature >= this.inflammationTreshold)
                {
                    firePS.Play();
                    this.isBurning = true;
                }
            }
        }
    }

    void OnCollisionTick(Water otherObject)
    {
        if (this.isFlammable)
        {
            this.temperature = Mathf.Max(minimumTemperature, this.temperature - otherObject.intensity);
            
            if (this.isBurning && this.temperature <= this.extinguishTreshold)
            {
                firePS.Stop();
                this.isBurning = false;
            }
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
                //OnCollisionTick(water.GetComponent<Water>());
            }
        }
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
