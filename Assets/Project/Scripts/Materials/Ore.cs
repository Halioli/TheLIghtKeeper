using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum OreState { WHOLE, BROKEN };
public enum Hardness { NORMAL, HARD };

public class Ore : MonoBehaviour
{
    // Private Attributes
    protected OreState breakState;
    protected HealthSystem healthSystem;
    protected int currentSpriteIndex;
    protected Sprite currentSprite;

    // Public Attributes
    [SerializeField] public Hardness hardness;
    public List<Sprite> spriteList;
    public ItemGameObject mineralItemToDrop;
    public ParticleSystem[] oreParticleSystem;

    private void Start()
    {
        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }
    }

    public bool CanBeMined() { return breakState == OreState.WHOLE; }

    public bool Broke() { return healthSystem.IsDead(); }

    public virtual void GetsMined(int damageAmount, int numberOfDrops)
    {
        transform.DOPunchScale(new Vector3(-0.6f, -0.6f, 0), 0.40f);
        // Damage the Ore
        healthSystem.ReceiveDamage(damageAmount);
        // Update ore Sprite
        ProgressNAmountOfSprites(damageAmount);

        if (healthSystem.IsDead())
        {
            breakState = OreState.BROKEN;

            // Drop mineralItemToDrop
            numberOfDrops = Random.Range(1, numberOfDrops);
            for (int i = 0; i < numberOfDrops; ++i)
            {
                DropMineralItem();
            }

            // Start disappear coroutine
            StartCoroutine("Disappear");
        }
        UpdateCurrentSprite();
        StartCoroutine("PlayBreakParticles");

    }

    protected void ProgressNAmountOfSprites(int numberOfProgressions)
    {
        if (currentSpriteIndex + numberOfProgressions >= spriteList.Count)
        {
            currentSpriteIndex = spriteList.Count - 1;
        }
        else
        {
            currentSpriteIndex += numberOfProgressions;
        }

        currentSprite = spriteList[currentSpriteIndex];
    }

    protected void DropMineralItem()
    {
        ItemGameObject droppedMineralItem = Instantiate(mineralItemToDrop, GetDropSpawnPosition(), Quaternion.identity);
        droppedMineralItem.DropsRandom();
    }

    protected Vector2 GetDropSpawnPosition()
    {
        return new Vector2(transform.position.x + 0.1f, transform.position.y);
    }

    protected void UpdateCurrentSprite()
    {
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    protected IEnumerator Disappear()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color transparentColor = spriteRenderer.material.color;
        transparentColor.a = 0.0f;

        Color semiTransparentColor = spriteRenderer.material.color;
        semiTransparentColor.a = 0.5f;

        spriteRenderer.material.color = semiTransparentColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = transparentColor;
        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }

    protected IEnumerator PlayBreakParticles()
    {
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Play();
        }
        yield return new WaitForSeconds(0.3f);
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }
    }
}
