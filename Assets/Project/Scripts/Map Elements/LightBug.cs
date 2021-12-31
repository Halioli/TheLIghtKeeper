using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightBug : MonoBehaviour
{
    Interpolator lerp;

    private Light2D[] pointLightBug;
    private bool isTurnedOn = false;

    void Start()
    {
        lerp = new Interpolator(5f, Interpolator.Type.SMOOTH);
        pointLightBug = GetComponentsInChildren<Light2D>();
    }

    void Update()
    {
        LightBugMovement();
        if (isTurnedOn)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }

    private void LightBugMovement()
    {
        lerp.Update(Time.deltaTime);
        if (lerp.isMinPrecise)
            lerp.ToMax();
        else if (lerp.isMaxPrecise)
            lerp.ToMin();

        transform.position = new Vector3(10f - 20f * lerp.Value, 0f, 0f);
    }

    IEnumerator FadeIn()
    {
        Interpolator lightFadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        lightFadeLerp.ToMax();
        while (!lightFadeLerp.isMaxPrecise)
        {
            lightFadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            pointLightBug[0].intensity = lightFadeLerp.Inverse + 0.5f;
            pointLightBug[1].intensity = lightFadeLerp.Inverse * 0.6f;
            //WAIT A FRAME
            yield return null;
        }

        isTurnedOn = true;
    }

    IEnumerator FadeOut()
    {
        Interpolator lightFadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        lightFadeLerp.ToMax();

        while (!lightFadeLerp.isMaxPrecise)
        {
            lightFadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            pointLightBug[0].intensity = lightFadeLerp.Value + 0.5f;
            pointLightBug[1].intensity = (lightFadeLerp.Value + 0.3f) * 0.6f;
            //WAIT A FRAME
            yield return null;
        }

        isTurnedOn = false;
    }
}
