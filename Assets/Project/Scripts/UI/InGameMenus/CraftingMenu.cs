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
    //public GameObject requiredMaterialPrefab;
    
    CraftingRecepieDisplayer craftingRecepieDisplayer;
    [SerializeField] GameObject craftingRecepieDisplayerGameObject;


    private void Start()
    {
        recepieButtonsGameObjects = new List<GameObject>();
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
        CraftingSystem.OnReceipeCraftingSuccess += ResetRecepieDisplayer;
    }

    private void OnDisable()
    {
        CraftableItemButton.OnHoverRecepieButton -= ResetRecepieDisplayer;
        CraftableItemButton.OnRecepieButtonHoverExit -= HideRecepieDisplayer;
        CraftingSystem.OnReceipeCraftingSuccess -= ResetRecepieDisplayer;
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

    private void SetFirstElemtTextToRed()
    {
        recepieButtonsGameObjects[0].GetComponentsInChildren<TextMeshProUGUI>()[2].color = Color.red;
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




}