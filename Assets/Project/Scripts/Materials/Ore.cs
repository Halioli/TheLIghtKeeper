using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum OreState { WHOLE, BROKEN };
public enum Hardness { NORMAL, HARD, VERY_HARD };

public class Ore : MonoBehaviour
{
    // Private Attributes
    protected OreState breakState;
    protected HealthSystem healthSystem;
    protected int currentSpriteIndex;
    protected Sprite currentSprite;

    // Public Attributes
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] protected Transform spriteTransform;
    [SerializeField] public Hardness hardness;
    public List<Sprite> spriteList;
    public ItemGameObject mineralItemToDrop;
    public ParticleSystem[] oreParticleSystem;

    public bool hasBeenMined = true;

    public delegate void OreGetsMinedAction();
    public static event OreGetsMinedAction playerMinesOreEvent;
    public static event OreGetsMinedAction playerBreaksOreEvent;


    private void Awake()
    {
        SaveSystem.ores.Add(this);
       
        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }
    }

    private void Update()
    {
        if (hasBeenMined)
        {
            gameObject.SetActive(false);
            breakState = OreState.BROKEN;
        }
    }

    public bool CanBeMined() { return breakState == OreState.WHOLE; }

    public bool Broke() { return healthSystem.IsDead(); }

    public virtual void GetsMined(int damageAmount, int numberOfDrops)
    {
        DamageTakeAnimation();
        // Damage the Ore
        healthSystem.ReceiveDamage(damageAmount);
        // Update ore Sprite
        ProgressNAmountOfSprites(damageAmount);

        if (healthSystem.IsDead())
        {
            hasBeenMined = true;
            breakState = OreState.BROKEN;
            OnDeathDamageTake();

            // Drop mineralItemToDrop
            numberOfDrops = Random.Range(1, numberOfDrops+1);
            for (int i = 0; i < numberOfDrops; ++i)
            {
                DropMineralItem();
            }

            // Start disappear coroutine
            StartCoroutine("Disappear");
        }
        else
        {
            OnDamageTake();
        }

        UpdateCurrentSprite();
        StartCoroutine("PlayBreakParticles");
    }

    protected virtual void DamageTakeAnimation()
    {
        spriteTransform.DOPunchScale(new Vector3(-0.6f, -0.6f, 0), 0.40f);
    }

    protected virtual void OnDamageTake()
    {
        if (playerMinesOreEvent != null) playerMinesOreEvent();
    }

    protected virtual void OnDeathDamageTake()
    {
        if (playerBreaksOreEvent != null) playerBreaksOreEvent();
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

    protected virtual void DropMineralItem()
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
        spriteRenderer.sprite = currentSprite;
    }

    protected IEnumerator Disappear()
    {
        Color transparentColor = spriteRenderer.material.color;
        transparentColor.a = 0.0f;

        Color semiTransparentColor = spriteRenderer.material.color;
        semiTransparentColor.a = 0.5f;

        spriteRenderer.material.color = semiTransparentColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = transparentColor;
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
        //Destroy(gameObject);
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
