using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class CircleLight : CustomLight
{
    private CircleCollider2D collider;
    private Light2D circleLight;

    [SerializeField] private float outerRadius = 2f;
    [SerializeField] private float innerRadius = 1.5f;
    private float radiusDifference;


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

    public override void Expand(float endIntensity)
    {
        if (lightState == LightState.EXPANDING) return;

        active = true;
        StartCoroutine(ExpandCircleLight());
        StartCoroutine(IntensityFadeIn(expandTime, endIntensity));
    }

    IEnumerator ExpandCircleLight()
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXPANDING;

        Interpolator expandLerp = new Interpolator(expandTime);
        expandLerp.ToMax();

        while (!expandLerp.isMaxPrecise)
        {
            expandLerp.Update(Time.deltaTime);

            lerpTransitionValue = expandLerp.Value * outerRadius;
            circleLight.pointLightOuterRadius = lerpTransitionValue;
            circleLight.pointLightInnerRadius = lerpTransitionValue > radiusDifference ? lerpTransitionValue - radiusDifference : 0f;

            yield return null;
        }

        lightState = LightState.NONE;
    }

    public override void Shrink(float endIntensity)
    {
        if (lightState == LightState.SHRINKING) return;

        active = false;
        StartCoroutine(ShrinkCircleLight());
        StartCoroutine(IntensityFadeOut(shrinkTime, endIntensity));
    }

    IEnumerator ShrinkCircleLight()
    {
        lightState = LightState.SHRINKING;

        Interpolator shrinkLerp = new Interpolator(shrinkTime);
        shrinkLerp.ToMax();

        while (!shrinkLerp.isMaxPrecise)
        {
            shrinkLerp.Update(Time.deltaTime);

            lerpTransitionValue = shrinkLerp.Inverse * outerRadius;
            circleLight.pointLightOuterRadius = lerpTransitionValue;
            circleLight.pointLightInnerRadius = lerpTransitionValue > radiusDifference ? lerpTransitionValue - radiusDifference : 0f;

            yield return null;
        }

        if (!active)
        {
            lightGameObject.SetActive(false);
        }
        lightState = LightState.NONE;
    }


    public override void SetIntensity(float intensity)
    {
        this.intensity = intensity;
        circleLight.intensity = intensity;
    }

    IEnumerator IntensityFadeIn(float interpolationTime, float endIntensity)
    {
        Interpolator intensityLerp = new Interpolator(interpolationTime);
        intensityLerp.ToMax();

        float intensityDifference = endIntensity - intensity;

        while (!intensityLerp.isMaxPrecise)
        {
            intensityLerp.Update(Time.deltaTime);

            circleLight.intensity = intensity;
            circleLight.intensity += intensityLerp.Value * intensityDifference;

            yield return null;
        }
        circleLight.intensity = endIntensity;
        intensity = endIntensity;
    }

    IEnumerator IntensityFadeOut(float interpolationTime, float endIntensity)
    {
        Interpolator intensityLerp = new Interpolator(interpolationTime);
        intensityLerp.ToMax();

        float intensityDifference = endIntensity - intensity;

        while (!intensityLerp.isMaxPrecise)
        {
            intensityLerp.Update(Time.deltaTime);

            circleLight.intensity = intensity;
            circleLight.intensity -= intensityLerp.Value * intensityDifference;

            yield return null;
        }
        circleLight.intensity = endIntensity;
        intensity = endIntensity;
    }


    public override void SetDistance(float distance)
    {
        circleLight.pointLightOuterRadius = distance;
        circleLight.pointLightInnerRadius = distance - radiusDifference;

        SetColliderRadiusMatchLightOuterRadius();
    }


    private void SetColliderRadiusMatchLightOuterRadius()
    {
        collider.radius = circleLight.pointLightOuterRadius;
    }


}
