using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class PlayerCircleLight : CircleLight
{
    private void Awake()
    {
        collider = lightGameObject.GetComponent<CircleCollider2D>();
        circleLight = lightGameObject.GetComponent<Light2D>();

        radiusDifference = outerRadius - innerRadius;
        intensity = circleLight.intensity;
    }

    private void Start()
    {
        SetColliderRadiusMatchLightOuterRadius();
    }

    private void OnEnable()
    {
        Lamp.turnOffLanternEvent += DisableCollision;
        Lamp.turnOnLanternEvent += EnableCollision;
    }

    private void OnDisable()
    {
        Lamp.turnOffLanternEvent -= DisableCollision;
        Lamp.turnOnLanternEvent -= EnableCollision;
    }


    private void DisableCollision()
    {
        circleLight.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void EnableCollision()
    {
        circleLight.GetComponent<CircleCollider2D>().enabled = true;
    }



    protected override bool ExpandCorrectionCheck()
    {
        if (PlayerLightChecker.playerInLight) return false;

        return base.ExpandCorrectionCheck();
    }


}
