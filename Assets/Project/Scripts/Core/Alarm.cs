using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Alarm : MonoBehaviour
{
    private const float MAX_INTENSITY = 0.8f;
    private const float MIN_INTENSITY = 0.4f;
    private const float DURATION = 2f;

    private float lightIntensity = 0.6f;
    private bool lightAtMin = true;
    private bool lightAtMax = false;

    [SerializeField] CustomLight customLight;

    void Update()
    {
        if (lightAtMin)
        {
            StartCoroutine(ChangeIntensityToMax());
            lightAtMin = false;
        }
        else if (lightAtMax)
        {
            StartCoroutine(ChangeIntensityToMin());
            lightAtMax = false;
        }

        customLight.SetIntensity(lightIntensity);

    }

    IEnumerator ChangeIntensityToMin()
    {
        float elapsed = 0.0f;
        while (elapsed < DURATION)
        {
            lightIntensity = Mathf.Lerp(MAX_INTENSITY, MIN_INTENSITY, elapsed / DURATION);
            elapsed += Time.deltaTime;
            yield return null;
        }
        lightIntensity = MIN_INTENSITY;
        lightAtMin = true;
    }

    IEnumerator ChangeIntensityToMax()
    {
        float elapsed = 0.0f;
        while (elapsed < DURATION)
        {
            lightIntensity = Mathf.Lerp(MIN_INTENSITY, MAX_INTENSITY, elapsed / DURATION);
            elapsed += Time.deltaTime;
            yield return null;
        }
        lightIntensity = MAX_INTENSITY;
        lightAtMax = true;
    }
}
