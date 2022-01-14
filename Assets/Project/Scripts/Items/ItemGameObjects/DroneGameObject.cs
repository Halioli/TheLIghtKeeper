using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroneGameObject : ItemGameObject
{
    [SerializeField] DroneInteractStation droneInteractStation;
    private bool inventoryIsOpen = false;


    void Start()
    {
        Instantiate(droneInteractStation, transform);
    }

    void Update()
    {

    }


    private void Leave()
    {
        transform.DOScaleY(0.5f, 0.4f);

    }

}
