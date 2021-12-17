using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class Torch : InteractStation
{
    public ParticleSystem initialFireParticles;
    public Light2D torchLight;

    private bool turnedOn = false;
    public TextMeshProUGUI interactText;

    public float torchIntensity = 1f;
    public float turnedOffIntensity = 0f;

    private float innerOuterRadiusOff = 0f;
    private const float innerRadiusOn = 1.8f;
    private const float outerRadiusOn = 3.3f;

    private const float LIGHT_CIRCLE_RADIUS_DIFFERENCE = outerRadiusOn - innerRadiusOn;

    private float timeToChangeRadius = 1f;
    private float timeElapsed = 0;


    // Start is called before the first frame update
    void Start()
    {
        initialFireParticles.Stop();
        if (!turnedOn)
        {
            torchLight.intensity = turnedOffIntensity;
        }
        interactText.alpha = 0f;
    }

    private void Update()
    {
        if (playerInsideTriggerArea)
        {
            interactText.alpha = 1f;
            GetInput();
        }
        else
        {
            interactText.alpha = 0f;
        }
    }
    public override void StationFunction()
    {
        if (!turnedOn)
        {
            SetTorchLightOn();
          
        }
        else
        {
            SetTorchLightOff();
        }
    }

    IEnumerator PlayFireParticleSystem()
    {
        initialFireParticles.Play();
        yield return new WaitForSeconds(1f);
        initialFireParticles.Stop();
    }

    void SetTorchLightOff()
    {
        StartCoroutine(PlayFireParticleSystem());
        torchLight.intensity = 1f;
        timeElapsed = 0;
        turnedOn = false;
    }

    void SetTorchLightOn()
    {
        StartCoroutine(PlayFireParticleSystem());
        torchLight.intensity = 1f;
        float timeElapsed = 0;
        if(timeElapsed < timeToChangeRadius)
        {
            torchLight.pointLightInnerRadius = Mathf.Lerp(innerOuterRadiusOff, innerRadiusOn, timeElapsed / timeToChangeRadius);
            torchLight.pointLightOuterRadius = Mathf.Lerp(innerOuterRadiusOff, outerRadiusOn, timeElapsed / timeToChangeRadius);
            timeElapsed += Time.deltaTime;
        }
        turnedOn = true;
    }


}
