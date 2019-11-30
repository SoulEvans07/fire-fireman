using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    // is flammable
    public bool isFlammable = true;
    public bool canSpreadFireToOthers = true;

    public float health = 100f;
    public float temperature = 0;
    public bool isBurning = false;

    public float heatResistance = 1;
    public float burnIntensity = 1;

    public float inflammationTreshold = 90f;
    public float extinguishTreshold = 70f;

    public ParticleSystem firePS;
    public Collider2D fireCollider;

    private float tickInterval = 0.1f;

    private float updateTimer = 0f;
    private Dictionary<GameObject, int> collidedBurnables = new Dictionary<GameObject, int>();


    // Start is called before the first frame update
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != this.gameObject && 
            (other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Player"))) 
        {
            if (collidedBurnables.ContainsKey(other.gameObject))
            {
                collidedBurnables[other.gameObject]++;
            }
            else
            {
                collidedBurnables.Add(other.gameObject, 1);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != this.gameObject && 
            (other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Player"))) 
        {
            collidedBurnables[other.gameObject]--;
        }
    }

    void OnTick()
    {
        if (health > 0)
        {
            if (isBurning)
            {
                health--;
            }
        }
    }

    void OnCollisionTick(Burnable otherObject)
    {
        if (this.isFlammable && otherObject.isBurning && otherObject.canSpreadFireToOthers)
        {
            this.temperature = Mathf.Min(inflammationTreshold, this.temperature + otherObject.burnIntensity / this.heatResistance);

            Debug.Log("Something is heating up");

            if (this.isBurning == false && this.temperature >= this.inflammationTreshold)
            {
                firePS.Play();
                this.isBurning = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer > tickInterval)
        {
            updateTimer = updateTimer - tickInterval;
            OnTick();

            foreach (KeyValuePair<GameObject, int> burnableTuple in collidedBurnables)
            {
                if (burnableTuple.Value > 0) 
                {
                    OnCollisionTick(burnableTuple.Key.gameObject.GetComponent<Burnable>());
                }
            }
        }
    }
}
