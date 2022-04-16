using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ConeLight : CustomLight
{
    private PolygonCollider2D collider;
    protected Light2D coneLight;

    [SerializeField] private float startDistance = 4f;
    private const float DISTANCE_DIFFERENCE = 1f;
    private const float RADIUS_DIFFERENCE = 15f;
    [SerializeField] private float lightAngle = 75f;


    private void Awake()
    {
        Init();
    }


    private void Start()
    {
        SetColliderFitLightOuterRadius();
    }



    protected override void Init()
    {
        collider = lightGameObject.GetComponent<PolygonCollider2D>();
        coneLight = lightGameObject.GetComponent<Light2D>();

        SetDistance(startDistance);
        SetAngle(lightAngle);

        intensity = coneLight.intensity;
    }


    public override void Expand(float endIntensity)
    {
        if (lightState == LightState.EXPANDING) return;

        StopCoroutine(ShrinkConeLight());
        StopCoroutine(IntensityFadeOut(0,0));

        active = true;
        StartCoroutine(ExpandConeLight());
        StartCoroutine(IntensityFadeIn(expandTime, endIntensity));
    }

    IEnumerator ExpandConeLight()
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXPANDING;

        Interpolator expandLerp = new Interpolator(expandTime);
        expandLerp.ToMax();

        while (!expandLerp.isMaxPrecise)
        {
            expandLerp.Update(Time.deltaTime);

            lerpTransitionValue = expandLerp.Value * lightAngle;
            coneLight.pointLightOuterAngle = lerpTransitionValue;
            coneLight.pointLightInnerAngle = (lerpTransitionValue >= RADIUS_DIFFERENCE ? lerpTransitionValue - RADIUS_DIFFERENCE : 0f);

            yield return null;
        }

        SetColliderFitLightOuterRadius();

        lightState = LightState.NONE;
    }

    public void ExtraExpand(float startLightAngle, float finalLightAngle, float endIntensity) 
    {
        if (lightState == LightState.EXTRA_EXPANDING) return;

        StopCoroutine(ExpandConeLight());
        StopCoroutine(ShrinkConeLight());
        StopCoroutine(IntensityFadeIn(0, 0));
        StopCoroutine(IntensityFadeOut(0, 0));

        active = true;
        StartCoroutine(ExtraExpandConeLight(startLightAngle, finalLightAngle));
        StartCoroutine(IntensityFadeIn(extraExpandTime, endIntensity));
    }

    IEnumerator ExtraExpandConeLight(float startLightAngle, float finalLightAngle)
    {
        lightGameObject.SetActive(true);
        lightState = LightState.EXTRA_EXPANDING;

        Interpolator expandLerp = new Interpolator(extraExpandTime);
        expandLerp.ToMax();

        float angleDifference = finalLightAngle - startLightAngle;

        while (!expandLerp.isMaxPrecise)
        {
            expandLerp.Update(Time.deltaTime);

            lerpTransitionValue = (expandLerp.Value * angleDifference) + startLightAngle;
            coneLight.pointLightOuterAngle = lerpTransitionValue;
            coneLight.pointLightInnerAngle = (lerpTransitionValue >= RADIUS_DIFFERENCE ? lerpTransitionValue - RADIUS_DIFFERENCE : 0f);

            yield return null;
        }

        SetColliderFitLightOuterRadius();

        lightState = LightState.NONE;
    }

    public override void Shrink(float endIntensity)
    {
        if (lightState == LightState.SHRINKING) return;

        StopCoroutine(ExpandConeLight());
        StopCoroutine(IntensityFadeIn(0, 0));

        active = false;
        StartCoroutine(ShrinkConeLight());
        StartCoroutine(IntensityFadeOut(shrinkTime, endIntensity));
    }

    IEnumerator ShrinkConeLight()
    {
        lightState = LightState.SHRINKING;

        Interpolator shrinkLerp = new Interpolator(shrinkTime);
        shrinkLerp.ToMax();

        while (!shrinkLerp.isMaxPrecise)
        {
            shrinkLerp.Update(Time.deltaTime);

            lerpTransitionValue = shrinkLerp.Inverse * lightAngle;
            coneLight.pointLightOuterAngle = lerpTransitionValue;
            coneLight.pointLightInnerAngle = (lerpTransitionValue <= RADIUS_DIFFERENCE ? 0f : lerpTransitionValue - RADIUS_DIFFERENCE);

            yield return null;
        }


        if (!active)
            lightGameObject.SetActive(false);

        lightState = LightState.NONE;
    }

    public void PartialShrink(float startLightAngle, float finalLightAngle, float endIntensity)
    {
        if (lightState == LightState.PARTIAL_SHRINKING) return;

        StopCoroutine(ExpandConeLight());
        StopCoroutine(ShrinkConeLight());
        StopCoroutine(IntensityFadeIn(0, 0));
        StopCoroutine(IntensityFadeOut(0, 0));

        active = true;
        StartCoroutine(PartialShrinkConeLight(startLightAngle, finalLightAngle));
        StartCoroutine(IntensityFadeOut(partialShrinkTime, endIntensity));
    }

    IEnumerator PartialShrinkConeLight(float startLightAngle, float finalLightAngle)
    {
        lightState = LightState.PARTIAL_SHRINKING;

        Interpolator shrinkLerp = new Interpolator(partialShrinkTime);
        shrinkLerp.ToMax();

        float angleDifference = finalLightAngle - startLightAngle; // negative difference

        while (!shrinkLerp.isMaxPrecise)
        {
            shrinkLerp.Update(Time.deltaTime);

            lerpTransitionValue = startLightAngle + (shrinkLerp.Value * angleDifference);
            coneLight.pointLightOuterAngle = lerpTransitionValue;
            coneLight.pointLightInnerAngle = (lerpTransitionValue <= RADIUS_DIFFERENCE ? 0f : lerpTransitionValue - RADIUS_DIFFERENCE);

            yield return null;
        }


        if (!active)
            lightGameObject.SetActive(false);

        SetColliderFitLightOuterRadius();

        lightState = LightState.NONE;
    }


    public override void SetIntensity(float intensity)
    {
        this.intensity = intensity;
        coneLight.intensity = intensity;
    }

    IEnumerator IntensityFadeIn(float interpolationTime, float endIntensity)
    {
        Interpolator intensityLerp = new Interpolator(interpolationTime);
        intensityLerp.ToMax();

        float intensityDifference = endIntensity - intensity;

        while (!intensityLerp.isMaxPrecise)
        {
            intensityLerp.Update(Time.deltaTime);

            coneLight.intensity = intensity;
            coneLight.intensity += intensityLerp.Value * intensityDifference;

            yield return null;
        }
        coneLight.intensity = endIntensity;
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

            coneLight.intensity = intensity;
            coneLight.intensity -= intensityLerp.Value * intensityDifference;

            yield return null;
        }
        coneLight.intensity = endIntensity;
        intensity = endIntensity;
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


    protected virtual void SetColliderFitLightOuterRadius()
    {
        float sizeY = coneLight.pointLightOuterRadius;
        float sixeX = Mathf.Sin(coneLight.pointLightOuterAngle) * sizeY;

        collider.points = new[] { new Vector2(sixeX / 2, sizeY), new Vector2(-sixeX / 2, sizeY), Vector2.zero };
    }


}
