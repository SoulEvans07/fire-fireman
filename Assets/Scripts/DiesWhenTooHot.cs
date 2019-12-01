using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesWhenTooHot : MonoBehaviour
{
    public float dyingTemperature = 80f;

    private Burnable burnable;
    private SpriteRenderer spriteRenderer;
 
    public Sprite deathSprite;
    private Sprite originalSprite;

    public bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        burnable = GetComponent<Burnable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        Initialize();
    }

    void Initialize()
    {
        if (ShouldDie() || isDead)
        {
            Kill();
        }
    }

    void Kill()
    {
        isDead = true;
        spriteRenderer.sprite = deathSprite;
    }

    void Resurrect()
    {
        isDead = false;
        spriteRenderer.sprite = originalSprite;
    }

    bool ShouldDie() => burnable.temperature >= dyingTemperature;

    // Update is called once per frame
    void Update()
    {
        if (!isDead && ShouldDie())
        {
            Kill();
        }
    }
}
