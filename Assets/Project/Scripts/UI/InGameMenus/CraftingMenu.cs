using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingMenu : MonoBehaviour
{
    // Private Attribute
    private Inventory playerInventory;
    private CraftingSystem craftingSystem;
    private List<GameObject> recepieButtonsGameObjects;
    private bool craftingRecepiesShown;

    // Public Attribute
    public InventoryMenu inventoryMenu;
    public GameObject craftingList;
    public GameObject buttonPrefab;
    public GameObject requiredMaterialPrefab;
    public Sprite smallCraftingRecepieFrame;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        craftingSystem = GameObject.FindGameObjectWithTag("CraftingStation").GetComponent<CraftingSystem>();
        recepieButtonsGameObjects = new List<GameObject>();
        craftingRecepiesShown = false;
    }

    private void Update()
    {
        if (!craftingRecepiesShown)
        {
            UpdateCraftingMenu();
            craftingRecepiesShown = true;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetFirstElemtTextToRed();
        }

        //craftingSystem.UpdatePlayerInventoryData();
        inventoryMenu.UpdateInventory();
    }

    private void UpdateCraftingMenu()
    {
        recepieButtonsGameObjects.Clear();
        int buttonNumb = 0;

        foreach (Recepie recepie in craftingSystem.availableRecepies)
        {
            GameObject gameObjectButton = Instantiate(buttonPrefab, craftingList.transform);
            recepieButtonsGameObjects.Add(gameObjectButton);

            gameObjectButton.GetComponent<Image>().sprite = smallCraftingRecepieFrame;
            gameObjectButton.GetComponent<CraftableItemButton>().buttonNumber = buttonNumb;
            gameObjectButton.GetComponentsInChildren<TextMeshProUGUI>()[0].text = recepie.recepieName;
            gameObjectButton.GetComponentsInChildren<Image>()[1].sprite = recepie.resultingItemUnit.GetItemSprite();
            gameObjectButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = recepie.resultingAmountUnit.ToString();

            for (int i = 0; i < recepie.requiredItemsList.Count; ++i)
            {
                GameObject requiredMaterial = Instantiate(requiredMaterialPrefab, gameObjectButton.GetComponentInChildren<HorizontalLayoutGroup>().transform);

                requiredMaterial.GetComponentInChildren<Image>().sprite = recepie.requiredItemsList[i].GetItemSprite();
                requiredMaterial.GetComponentInChildren<TextMeshProUGUI>().text = recepie.requiredAmountsList[i].ToString();
            }

            ++buttonNumb;
        }
    }

    private void SetFirstElemtTextToRed()
    {
        recepieButtonsGameObjects[0].GetComponentsInChildren<TextMeshProUGUI>()[2].color = Color.red;
    }
}
