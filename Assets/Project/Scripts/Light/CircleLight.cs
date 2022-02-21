using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class CircleLight : CustomLight
{
    protected CircleCollider2D collider;
    protected Light2D circleLight;

    [SerializeField] protected float outerRadius = 2f;
    [SerializeField] protected float innerRadius = 1.5f;
    protected float radiusDifference;


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
        StartCoroutine(ExpandCorrection(endIntensity));
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

    IEnumerator ExpandCorrection(float endIntensity)
    {
        yield return new WaitForSeconds(expandTime + 0.1f);
        
        if (lightState == LightState.NONE && circleLight.pointLightOuterRadius != outerRadius)
        {
            Expand(endIntensity);
        }
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

    public void IntensityFadeInTransition(float endIntensity)
    {
        StartCoroutine(IntensityFadeIn(extraExpandTime, endIntensity));
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

    public void IntensityFadeOutTransition(float endIntensity)
    {
        StartCoroutine(IntensityFadeOut(partialShrinkTime, endIntensity));
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


    protected void SetColliderRadiusMatchLightOuterRadius()
    {
        collider.radius = circleLight.pointLightOuterRadius;
    }


}
