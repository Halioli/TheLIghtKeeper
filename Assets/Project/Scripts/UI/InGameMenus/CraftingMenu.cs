using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingMenu : MonoBehaviour
{
    private readonly int MAX_REQUIRED_MATERIALS = 3;

    // Private Attribute
    private Inventory playerInventory;
    [SerializeField] private CraftingSystem craftingSystem;
    private List<GameObject> recepieButtonsGameObjects;
    private RectTransform craftingListRectTransform;
    private bool updatedCraftingMenu;

    // Public Attribute
    public GameObject craftingList;
    public GameObject buttonPrefab;
    //public GameObject requiredMaterialPrefab;
    
    CraftingRecepieDisplayer craftingRecepieDisplayer;
    [SerializeField] GameObject craftingRecepieDisplayerGameObject;


    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        recepieButtonsGameObjects = new List<GameObject>();
        craftingListRectTransform = craftingList.GetComponent<RectTransform>();
        updatedCraftingMenu = false;

        craftingRecepieDisplayer = craftingRecepieDisplayerGameObject.GetComponent<CraftingRecepieDisplayer>();

        HideRecepieDisplayer();
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


    private void OnEnable()
    {
        CraftableItemButton.OnHoverRecepieButton += ResetRecepieDisplayer;
        CraftableItemButton.OnRecepieButtonHoverExit += HideRecepieDisplayer;
    }

    private void OnDisable()
    {
        CraftableItemButton.OnHoverRecepieButton -= ResetRecepieDisplayer;
        CraftableItemButton.OnRecepieButtonHoverExit -= HideRecepieDisplayer;
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

            //RectTransform gameObjectButtonRectTransform = gameObjectButton.GetComponent<RectTransform>();
            //craftingListRectTransform.sizeDelta = new Vector2(craftingListRectTransform.sizeDelta.x,
            //    craftingListRectTransform.sizeDelta.y + gameObjectButtonRectTransform.sizeDelta.y);

            //gameObjectButton.GetComponent<CraftableItemButton>().buttonNumber = buttonNumber;
            //gameObjectButton.GetComponent<CraftableItemButton>().SetDescription(recepie.resultingItemUnit.description);
            //gameObjectButton.GetComponentsInChildren<TextMeshProUGUI>()[0].text = recepie.recepieName;
            //gameObjectButton.GetComponentsInChildren<Image>()[1].sprite = recepie.resultingItemUnit.GetItemSprite();
            //gameObjectButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = recepie.resultingAmountUnit.ToString();

            //craftingRecepieDisplayer.SetRecepieResultingItem(recepie.resultingItemUnit.ID);

            //craftingRecepieDisplayer.ClearCurrentRequiredMaterials();
            //for (int i = 0; i < recepie.requiredItemsList.Count; ++i)
            //{
            //    craftingRecepieDisplayer.AddRequiredMaterial(recepie.requiredItemsList[i].ID, recepie.requiredAmountsList[i]);

            //    //GameObject requiredMaterial = Instantiate(requiredMaterialPrefab, gameObjectButton.GetComponentInChildren<HorizontalLayoutGroup>().transform);

            //    //requiredMaterial.GetComponentInChildren<Image>().sprite = recepie.requiredItemsList[i].GetItemSprite();
            //    //requiredMaterial.GetComponentInChildren<TextMeshProUGUI>().text = recepie.requiredAmountsList[i].ToString();
            //}

            ++buttonNumber;
        }
        //craftingList.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, buttonPrefab.GetComponent<RectTransform>().sizeDelta.y * recepieButtonsGameObjects.Count);
    }

    private void SetFirstElemtTextToRed()
    {
        recepieButtonsGameObjects[0].GetComponentsInChildren<TextMeshProUGUI>()[2].color = Color.red;
    }

    public void ShowRecepies()
    {
        updatedCraftingMenu = false;
    }


    private void ResetRecepieDisplayer(int recepieIndex)
    {
        craftingRecepieDisplayerGameObject.SetActive(true);

        Recepie recepie = craftingSystem.availableRecepies[recepieIndex];
        craftingRecepieDisplayer.SetRecepieResultingItem(recepie.resultingItemUnit.ID, recepie.resultingAmountUnit);


        for (int i = 0; i < recepie.requiredItemsList.Count; ++i)
        {
            craftingRecepieDisplayer.AddRequiredMaterial(i, recepie.requiredItemsList[i].ID, recepie.requiredAmountsList[i]);
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




}