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
    private RectTransform craftingListRectTransform;
    private bool updatedCraftingMenu;

    // Public Attribute
    public GameObject craftingList;
    public GameObject buttonPrefab;
    public GameObject requiredMaterialPrefab;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        craftingSystem = GameObject.FindGameObjectWithTag("CraftingStation").GetComponent<CraftingSystem>();
        recepieButtonsGameObjects = new List<GameObject>();
        craftingListRectTransform = craftingList.GetComponent<RectTransform>();
        updatedCraftingMenu = false;
    }

    private void Update()
    {
        if (!updatedCraftingMenu)
        {
            UpdateCraftingMenu();
            updatedCraftingMenu = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetFirstElemtTextToRed();
        }

    }

    private void UpdateCraftingMenu()
    {
        foreach (GameObject recepieButton in recepieButtonsGameObjects)
        {
            Destroy(recepieButton);
        }
        recepieButtonsGameObjects.Clear();


        int buttonNumb = 0;
        foreach (Recepie recepie in craftingSystem.availableRecepies)
        {
            GameObject gameObjectButton = Instantiate(buttonPrefab, craftingList.transform);

            recepieButtonsGameObjects.Add(gameObjectButton);

            RectTransform gameObjectButtonRectTransform = gameObjectButton.GetComponent<RectTransform>();
            craftingListRectTransform.sizeDelta = new Vector2(craftingListRectTransform.sizeDelta.x,
                craftingListRectTransform.sizeDelta.y + gameObjectButtonRectTransform.sizeDelta.y);

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
        craftingList.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, buttonPrefab.GetComponent<RectTransform>().sizeDelta.y * recepieButtonsGameObjects.Count);
    }

    private void SetFirstElemtTextToRed()
    {
        recepieButtonsGameObjects[0].GetComponentsInChildren<TextMeshProUGUI>()[2].color = Color.red;
    }

    public void ShowRecepies()
    {
        updatedCraftingMenu = false;
    }
}