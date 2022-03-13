using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGameObject : ItemGameObject
{
    [SerializeField] GameObject bombAuxiliar;


    public override void DoFunctionality()
    {
        Instantiate(bombAuxiliar, PlayerInputs.instance.transform.position, Quaternion.identity);
    }

}
