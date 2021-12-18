using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Light : MonoBehaviour
{
    Light2D light2D;
    float LIGHT_OUTER_RADIUS;
    float LIGHT_RADIUS_DIFFERENCE;

    public void Awake()
    {
        light2D = GetComponent<Light2D>();
        LIGHT_OUTER_RADIUS = GetComponent<Light2D>().pointLightOuterRadius;
        LIGHT_RADIUS_DIFFERENCE = LIGHT_OUTER_RADIUS - GetComponent<Light2D>().pointLightInnerRadius;
    }


    public void Appear()
    {
        StartCoroutine(LightAppears());
    }

    public void Disappear()
    {
        StartCoroutine(LightDisappears());
    }

    IEnumerator LightAppears()
    {
        GetComponent<Light2D>().pointLightOuterRadius = 0;
        GetComponent<Light2D>().pointLightInnerRadius = 0;
        for (float i = 0f; i < LIGHT_OUTER_RADIUS; i += LIGHT_OUTER_RADIUS * Time.deltaTime * 8)
        {
            GetComponent<Light2D>().pointLightOuterRadius = i;
            GetComponent<Light2D>().pointLightInnerRadius = i > LIGHT_RADIUS_DIFFERENCE ? i - LIGHT_RADIUS_DIFFERENCE : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator LightDisappears()
    {
        for (float i = LIGHT_OUTER_RADIUS; i > 0f; i -= Time.deltaTime * LIGHT_OUTER_RADIUS * 8)
        {
            GetComponent<Light2D>().pointLightOuterRadius = i;
            GetComponent<Light2D>().pointLightInnerRadius = i > LIGHT_RADIUS_DIFFERENCE ? i - LIGHT_RADIUS_DIFFERENCE : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
