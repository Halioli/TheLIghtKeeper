using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightRodGameObject : ItemGameObject
{
    [SerializeField] GameObject lightRodAuxiliar;


    public override void DoFunctionality()
    {
        Instantiate(lightRodAuxiliar, PlayerInputs.instance.transform.position, Quaternion.identity);
    }


}
