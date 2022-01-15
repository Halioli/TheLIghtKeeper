using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class CircleLight : CustomLight
{
    private Light2D circleLight;

    [SerializeField] private float outerRadius = 2f;
    [SerializeField] private float innerRadius = 1.5f;
    private float radiusDifference;

    private void Awake()
    {
        circleLight = lightGameObject.GetComponent<Light2D>();

        radiusDifference = outerRadius - innerRadius;
    }



    public override void Expand()
    {
        if (lightState == LightState.EXPANDING) return;

        active = true;
        StartCoroutine(ExpandCircleLight());
    }

    IEnumerator ExpandCircleLight()
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXPANDING;

        for (float i = 0f; i < outerRadius; i += Time.deltaTime * outerRadius * 8)
        {
            circleLight.pointLightOuterRadius = i;
            circleLight.pointLightInnerRadius = i > radiusDifference ? i - radiusDifference : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        lightState = LightState.NONE;
    }

    public override void Shrink()
    {
        if (lightState == LightState.SHIRINKING) return;

        active = false;
        StartCoroutine(ShrinkCircleLight());
    }

    IEnumerator ShrinkCircleLight()
    {
        lightState = LightState.SHIRINKING;
        for (float i = outerRadius; i > 0f; i -= Time.deltaTime * outerRadius * 8)
        {
            circleLight.pointLightOuterRadius = i;
            circleLight.pointLightInnerRadius = i > radiusDifference ? i - radiusDifference : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }


        if (!active)
        {
            lightGameObject.SetActive(false);
        }
        lightState = LightState.NONE;
    }


    public override void SetIntensity(float intensity)
    {
        circleLight.intensity = intensity;
    }

    public override void SetDistance(float distance)
    {
        circleLight.pointLightOuterRadius = distance;
        circleLight.pointLightInnerRadius = distance - radiusDifference;
    }


}
