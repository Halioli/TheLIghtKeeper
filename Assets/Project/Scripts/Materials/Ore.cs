using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum OreState { WHOLE, BROKEN};

public class Ore : MonoBehaviour
{
    // Private Attributes
    private OreState breakState;
    private HealthSystem healthSystem;
    private int currentSpriteIndex;
    private Sprite currentSprite;

    // Public Attributes
    public List<Sprite> spriteList;
    public ItemGameObject mineralItemToDrop;


    private void Start()
    {
        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
    }



    public bool CanBeMined() { return breakState == OreState.WHOLE; }

    public bool Broke() { return healthSystem.IsDead(); }

    public void GetsMined(int damageAmount)
    {
        // Damage the Ore
        healthSystem.ReceiveDamage(damageAmount);
        // Update ore Sprite
        ProgressNAmountOfSprites(damageAmount);

        if (healthSystem.IsDead())
        {
            breakState = OreState.BROKEN;

            // Drop mineralItemToDrop
            DropMineralItem();

            // Start disappear coroutine
            StartCoroutine("Disappear");
        }

        UpdateCurrentSprite();
    }

    private void ProgressNAmountOfSprites(int numberOfProgressions)
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

    private void DropMineralItem()
    {
        ItemGameObject droppedMineralItem = Instantiate(mineralItemToDrop, GetDropSpawnPosition(), Quaternion.identity);
        droppedMineralItem.DropsDown();
        droppedMineralItem.StartDespawning();
    }

    private Vector2 GetDropSpawnPosition()
    {
        return new Vector2(transform.position.x + 0.1f, transform.position.y);
    }

    private void UpdateCurrentSprite()
    {
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    IEnumerator Disappear()
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


}
