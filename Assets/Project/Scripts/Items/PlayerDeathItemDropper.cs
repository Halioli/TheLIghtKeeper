using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathItemDropper : ItemDropper
{

    private void OnEnable()
    {
        PlayerHandler.OnPlayerDeath += DropItems;
    }

    private void OnDisable()
    {
        PlayerHandler.OnPlayerDeath -= DropItems;
    }

}
