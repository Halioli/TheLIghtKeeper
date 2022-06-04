using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightRodGameObject : ItemGameObject
{
    [SerializeField] GameObject lightRodAuxiliar;


    public override void DoFunctionality()
    {
        Vector2 spawnPosition = PlayerInputs.instance.transform.position;
        spawnPosition.y -= 0.5f;
        Instantiate(lightRodAuxiliar, spawnPosition, Quaternion.identity);
    }


}
