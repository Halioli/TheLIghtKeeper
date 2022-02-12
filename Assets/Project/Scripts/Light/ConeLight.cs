using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ConeLight : CustomLight
{
    private Light2D coneLight;

    [SerializeField] private float startDistance = 4f;
    private const float DISTANCE_DIFFERENCE = 5f;
    private const float RADIUS_DIFFERENCE = 20f;
    [SerializeField] private float lightAngle = 75f;



    private void Awake()
    {
        coneLight = lightGameObject.GetComponent<Light2D>();

        SetDistance(startDistance);
        SetAngle(lightAngle);
    }



    public override void Expand()
    {
        if (lightState == LightState.EXPANDING) return;

        StopCoroutine(ShrinkConeLight());

        active = true;
        StartCoroutine(ExpandConeLight());
    }

    IEnumerator ExpandConeLight()
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXPANDING;

        for (float i = 0f; i < lightAngle; i += Time.deltaTime * lightAngle * 4)
        {
            coneLight.pointLightOuterAngle = i;
            coneLight.pointLightInnerAngle = (i >= RADIUS_DIFFERENCE ? i - RADIUS_DIFFERENCE : 0f);
            yield return null;
        }

        lightState = LightState.NONE;
    }

    public void ExtraExpand(float expandedLightAngle) 
    {
        if (lightState == LightState.EXTRA_EXPANDING) return;

        StopCoroutine(ExpandConeLight());
        StopCoroutine(ShrinkConeLight());

        active = true;
        StartCoroutine(ExtraExpandConeLight(expandedLightAngle));
    }

    IEnumerator ExtraExpandConeLight(float expandedLightAngle)
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXTRA_EXPANDING;

        for (float i = lightAngle; i < expandedLightAngle; i += Time.deltaTime * lightAngle * 4)
        {
            coneLight.pointLightOuterAngle = i;
            coneLight.pointLightInnerAngle = (i >= RADIUS_DIFFERENCE ? i - RADIUS_DIFFERENCE : 0f);
            yield return null;
        }

        lightState = LightState.NONE;
    }

    public override void Shrink()
    {
        if (lightState == LightState.SHRINKING) return;

        StopCoroutine(ExpandConeLight());

        active = false;
        StartCoroutine(ShrinkConeLight());
    }

    IEnumerator ShrinkConeLight()
    {
        lightState = LightState.SHRINKING;
        for (float i = lightAngle; i > 0f; i -= Time.deltaTime * lightAngle * 8)
        {
            coneLight.pointLightOuterAngle = i;
            coneLight.pointLightInnerAngle = (i <= RADIUS_DIFFERENCE ? 0f : i - RADIUS_DIFFERENCE);
            yield return null;
        }

        if (!active)
            lightGameObject.SetActive(false);

        lightState = LightState.NONE;
    }

    public void PartialShrink(float expandedLightAngle)
    {
        if (lightState == LightState.PARTIAL_SHRINKING) return;

        StopCoroutine(ExpandConeLight());
        StopCoroutine(ShrinkConeLight());

        active = true;
        StartCoroutine(PartialShrinkConeLight(expandedLightAngle));
    }

    IEnumerator PartialShrinkConeLight(float expandedLightAngle)
    {
        lightState = LightState.PARTIAL_SHRINKING;
        for (float i = expandedLightAngle; i > lightAngle; i -= Time.deltaTime * lightAngle * 8)
        {
            coneLight.pointLightOuterAngle = i;
            coneLight.pointLightInnerAngle = (i <= RADIUS_DIFFERENCE ? 0f : i - RADIUS_DIFFERENCE);
            yield return null;
        }

        if (!active)
            lightGameObject.SetActive(false);

        lightState = LightState.NONE;
    }


    public override void SetIntensity(float intensity)
    {
        coneLight.intensity = intensity;
    }

    public override void SetDistance(float distance)
    {
        coneLight.pointLightInnerRadius = distance - DISTANCE_DIFFERENCE;
        coneLight.pointLightOuterRadius = distance;
    }

    public void SetAngle(float angle)
    {
        lightAngle = angle;

        coneLight.pointLightOuterAngle = lightAngle - RADIUS_DIFFERENCE;
        coneLight.pointLightOuterAngle = lightAngle;

    }

}
