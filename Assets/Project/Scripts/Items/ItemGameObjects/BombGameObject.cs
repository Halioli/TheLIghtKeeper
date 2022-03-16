using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGameObject : ItemGameObject
{
    [SerializeField] GameObject bombAuxiliar;


    public override void DoFunctionality()
    {
        Vector2 spawnPosition = PlayerInputs.instance.transform.position;
        spawnPosition.y -= 0.5f;
        Instantiate(bombAuxiliar, spawnPosition, Quaternion.identity);
    }

}
