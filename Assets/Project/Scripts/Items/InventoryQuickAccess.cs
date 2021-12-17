using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickAccess : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 1f;
    private const float INPUT_TIME = 3f;

    private Inventory playerInventory;
    private PlayerInputs playerInputs;
    private int mouseScrollDirection;
    
    private bool printedAlready = true;
    private bool alphaIsOne;
    private bool noInput;

    private HUDItem rightItem;
    private HUDItem centerItem;
    private HUDItem leftItem;
    private CanvasGroup quickAccessGroup;

    private List<ItemStack.itemStackToDisplay> itemsToDisplay;

    // Public Attributes
    public GameObject quickAccessGameObject;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
        mouseScrollDirection = 0;
        alphaIsOne = false;
        noInput = true;

        rightItem = quickAccessGameObject.GetComponentsInChildren<HUDItem>()[0];
        centerItem = quickAccessGameObject.GetComponentsInChildren<HUDItem>()[1];
        leftItem = quickAccessGameObject.GetComponentsInChildren<HUDItem>()[2];
        quickAccessGroup = quickAccessGameObject.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        mouseScrollDirection = (int)playerInputs.PlayerMouseScroll().y;

        if (mouseScrollDirection != 0)
        {
            noInput = false;
            StopCoroutine(WaitForInput(INPUT_TIME));

            if (!alphaIsOne)
                StartCoroutine(ChangeCanvasGroupAlphaToOne(quickAccessGroup));

            if (mouseScrollDirection > 0)
            {
                playerInventory.CycleRightSelectedItemIndex();
                printedAlready = false;
            }
            else if (mouseScrollDirection < 0)
            {
                playerInventory.CycleLeftSelectedItemIndex();
                printedAlready = false;
            }
        }
        else if (alphaIsOne && !noInput)
        {
            StartCoroutine(WaitForInput(INPUT_TIME));

            if (noInput)
                StartCoroutine(ChangeCanvasGroupAlphaToZero(quickAccessGroup));
        }
        
        if (!printedAlready)
        {
            Debug.Log(playerInventory.indexOfSelectedInventorySlot);
            printedAlready = true;
        }

        if (playerInputs.PlayerPressedUseButton())
        {
            playerInventory.UseSelectedConsumibleItem();
        }

        if (alphaIsOne)
            UpdateQuickAccessItems();
    }

    private void UpdateQuickAccessItems()
    {
        if (itemsToDisplay != null)
            itemsToDisplay.Clear();
        
        itemsToDisplay = playerInventory.Get3ItemsToDisplayInHUD();

        // Update Right Item
        rightItem.image.sprite = itemsToDisplay[0].sprite;

        // Update Center Item
        centerItem.image.sprite = itemsToDisplay[1].sprite;
        centerItem.textName.text = itemsToDisplay[1].name;
        centerItem.textQuantity.text = CheckTextForZeros(itemsToDisplay[1].quantity.ToString());

        // Update Left Item
        leftItem.image.sprite = itemsToDisplay[2].sprite;
    }

    private string CheckTextForZeros(string text)
    {
        string zero = "0";

        if (text.Length < 2)
        {
            text = zero + text;
        }

        return text;
    }

    IEnumerator ChangeCanvasGroupAlphaToZero(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        for (float t = 0f; t < FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;
        alphaIsOne = false;
    }

    IEnumerator ChangeCanvasGroupAlphaToOne(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;
        alphaIsOne = true;
    }

    IEnumerator WaitForInput(float secsToWait)
    {
        yield return new WaitForSeconds(secsToWait);
        noInput = true;
    }
}
