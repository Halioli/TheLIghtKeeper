using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoMiner : InteractStation
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject autoMinerCanvasGameObject;
    [SerializeField] InventoryMenu autoMinerInventoryMenu;
    private bool inventoryIsOpen = false;


    Item itemToMine;
    [SerializeField] float mineCooldown;
    float animationTime = 0.5f;
    Vector2 animationScale;
    private bool autoMinerStopped = true;


    // Actions
    public delegate void AutoMinerAction();
    public static event AutoMinerAction OnAutoMinerPlaced;
    public static event AutoMinerAction OnMineStart;
    public static event AutoMinerAction OnMineStop;
    public static event AutoMinerAction OnMine;


    private void Start()
    {
        animationScale = new Vector3(-transform.lossyScale.x/2, 0);
        autoMinerCanvasGameObject.SetActive(inventoryIsOpen);
    }

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from InteractStation 
        }
        else
        {
            if (inventoryIsOpen)
            {
                CloseInventory();
            }
        }

        if (autoMinerStopped && inventory.ItemCanBeAdded(itemToMine))
        {
            StartAutoMine();
        }
    }

    //private void OnEnable()
    //{
    //    Inventory.OnItemMove += autoMinerInventoryMenu.UpdateInventory;
    //}

    //private void OnDisable()
    //{
    //    Inventory.OnItemMove -= autoMinerInventoryMenu.UpdateInventory;
    //}



    override public void StationFunction()
    {
        if (inventoryIsOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }

    }

    private void OpenInventory()
    {
        DoOnInteractOpen();

        inventoryIsOpen = true;
        autoMinerCanvasGameObject.SetActive(true);
        autoMinerInventoryMenu.UpdateInventory();

        playerInventory.SetOtherInventory(this.inventory);
        this.inventory.SetOtherInventory(playerInventory);

        PauseMenu.PauseMineAndAttack();
    }

    private void CloseInventory()
    {
        DoOnInteractClose();

        inventoryIsOpen = false;
        autoMinerCanvasGameObject.SetActive(false);

        playerInventory.SetOtherInventory(null);
        this.inventory.SetOtherInventory(null);

        PauseMenu.ResumeMineAndAttack();
    }



    public void GetsPlacedDown(Item itemToMine)
    {
        PlacedAnimation();
        SetItemToMine(itemToMine);
    }

    public void SetItemToMine(Item itemToMine)
    {
        this.itemToMine = itemToMine;
    }


    private void StartAutoMine()
    {
        StopCoroutine(AutoMine());
        StartCoroutine(AutoMine());
    }

    IEnumerator AutoMine()
    {
        if (OnMineStart != null) {
            OnMineStart();
        }
        autoMinerStopped = false;

        while (inventory.ItemCanBeAdded(itemToMine))
        {
            yield return new WaitForSeconds(mineCooldown);

            if (inventory.ItemCanBeAdded(itemToMine)) // protect in case player adds item
            {
                MineItem();
                MineAnimation();
            }
        }

        if (OnMineStop != null) {
            OnMineStop();
        }
        autoMinerStopped = true;
    }


    private void MineItem()
    {
        inventory.AddItemToInventory(itemToMine);
    }


    private void PlacedAnimation()
    {
        transform.DOPunchScale(animationScale, animationTime, 10, 10);

        if (OnAutoMinerPlaced != null)
        {
            OnAutoMinerPlaced();
        }
    }

    private void MineAnimation()
    {
        transform.DOPunchScale(animationScale, animationTime, 10, 10);

        if (OnMine != null)
        {
            OnMine();
        }
    }

}
