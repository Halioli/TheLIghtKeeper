using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingMenu : MonoBehaviour
{
    private readonly int MAX_REQUIRED_MATERIALS = 3;

    // Private Attribute
    [SerializeField] private CraftingSystem craftingSystem;
    private List<GameObject> recepieButtonsGameObjects;
    private bool updatedCraftingMenu;

    // Public Attribute
    public GameObject craftingList;
    public GameObject buttonPrefab;

    [SerializeField] GameObject[] craftingButtons;
    [SerializeField] GameObject[] lockedTexts;
    int[] craftsPerUpgrade;
    int craftingButtonCount;
    private const int MAX_LEVEL = 5;
    private int currentLevel = 0;

    //public GameObject requiredMaterialPrefab;

    [SerializeField] CraftingRecepieDisplayer craftingRecepieDisplayer;
    [SerializeField] GameObject craftingRecepieDisplayerGameObject;


    private void Start()
    {
        recepieButtonsGameObjects = new List<GameObject>();
        updatedCraftingMenu = false;

        HideRecepieDisplayer();
    }

    //private void Update()
    //{
    //    if (!updatedCraftingMenu)
    //    {
    //        UpdateCraftingMenu();
    //        updatedCraftingMenu = true;
    //    }

    //}


    private void OnEnable()
    {
        CraftableItemButton.OnHoverRecepieButton += ResetRecepieDisplayer;
        CraftableItemButton.OnRecepieButtonHoverExit += HideRecepieDisplayer;
        CraftingSystem.OnReceipeCraftingSuccess += ResetRecepieDisplayer;

        //CoreUpgrade.OnCoreUpgrade += LevelUp;
    }

    private void OnDisable()
    {
        CraftableItemButton.OnHoverRecepieButton -= ResetRecepieDisplayer;
        CraftableItemButton.OnRecepieButtonHoverExit -= HideRecepieDisplayer;
        CraftingSystem.OnReceipeCraftingSuccess -= ResetRecepieDisplayer;

        //CoreUpgrade.OnCoreUpgrade += LevelUp;
    }



    private void UpdateCraftingMenu()
    {
        foreach (GameObject recepieButton in recepieButtonsGameObjects)
        {
            Destroy(recepieButton);
        }
        recepieButtonsGameObjects.Clear();



        int buttonNumber = 0;
        foreach (Recepie recepie in craftingSystem.availableRecepies)
        {
            GameObject gameObjectButton = Instantiate(buttonPrefab, craftingList.transform);

            recepieButtonsGameObjects.Add(gameObjectButton);
            gameObjectButton.GetComponent<CraftableItemButton>().Init(buttonNumber, recepie.resultingItemUnit.ID);

            ++buttonNumber;
        }
    }

    public void ShowRecepies()
    {
        updatedCraftingMenu = false;
    }


    // Called on hover && after click and craft success
    private void ResetRecepieDisplayer(int recepieIndex)
    {
        int[] amountsInInventory = new int[3];

        craftingSystem.TestSelectedReceipe(recepieIndex, amountsInInventory);



        craftingRecepieDisplayerGameObject.SetActive(true);

        Recepie recepie = craftingSystem.availableRecepies[recepieIndex];
        craftingRecepieDisplayer.SetRecepieResultingItem(recepie.resultingItemUnit.ID, recepie.resultingAmountUnit);



        for (int i = 0; i < recepie.requiredItemsList.Count; ++i)
        {
            craftingRecepieDisplayer.AddRequiredMaterial(i, recepie.requiredItemsList[i].ID, recepie.requiredAmountsList[i], amountsInInventory[i]);
        }
        for (int i = recepie.requiredItemsList.Count; i < MAX_REQUIRED_MATERIALS; ++i)
        {
            craftingRecepieDisplayer.ClearRequiredMaterial(i);
        }

    }

    private void HideRecepieDisplayer()
    {
        craftingRecepieDisplayerGameObject.SetActive(false);
    }




    // Must be called by CraftingSystem after initializing crafting receipes
    public void Init()
    {
        InitCraftsPerUpgrade();
        InitAllCrafts();
        InitCraftingButtons();
    }


    // Used by CraftingSystem to init values
    private void InitCraftsPerUpgrade()
    {
        craftsPerUpgrade = craftingSystem.GetCraftsPerUpgrade();
        //Example: craftsPerUpgrade = { 1, 2, 2, 1 }
    }

    private void InitAllCrafts()
    {
        int buttonNumber = 0;

        foreach (RecepieCollection recepieCollection in craftingSystem.recepiesLvl)
        {
            foreach (Recepie recepie in recepieCollection.recepies)
            {
                //Debug.Log("buttonNumber: "+buttonNumber);
                //Debug.Log("recepie: " + recepie.recepieName);
                //Debug.Log("resultingItem: " + recepie.resultingItemUnit.itemName);
                //Debug.Log("resultingItem: " + ItemLibrary.instance.GetItem(recepie.resultingItemUnit.ID).itemName);

                craftingButtons[buttonNumber].GetComponent<CraftableItemButton>().Init(buttonNumber, recepie.resultingItemUnit.ID);

                ++buttonNumber;
            }
        }

    }


    private void InitCraftingButtons()
    {
        currentLevel = 0;
        craftingButtonCount = craftsPerUpgrade[currentLevel];

        for (int i = 0; i < craftingButtonCount; ++i)
        {
            craftingButtons[i].GetComponent<Button>().interactable = true;
        }

        for (int i = craftingButtonCount; i < craftingButtons.Length; ++i)
        {
            craftingButtons[i].GetComponent<Button>().interactable = false;
        }

        lockedTexts[currentLevel].SetActive(false);
    }


    public void LevelUp()
    {
        ++currentLevel;

        if (currentLevel >= MAX_LEVEL) return;


        craftingButtonCount += craftsPerUpgrade[currentLevel];

        for (int i = 0; i < craftingButtonCount; ++i)
        {
            craftingButtons[i].GetComponent<Button>().interactable = true;
        }


        if (currentLevel < lockedTexts.Length) lockedTexts[currentLevel].SetActive(false);
    }



}