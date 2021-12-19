using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class Torch : InteractStation
{
    public ParticleSystem smokeTorchParticles;
    public Light2D torchLight;

    private bool turnedOn = false;
    public TextMeshProUGUI interactText;

    public float torchIntensity = 1f;
    public float turnedOffIntensity = 0f;

    private const float innerRadiusOn = 1.8f;
    private const float outerRadiusOn = 3.3f;

    public CircleCollider2D lightRadius;

    public Animator animTorch;

    // Start is called before the first frame update
    void Start()
    {
        smokeTorchParticles.Stop();
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
    void SetTorchLightOff()
    {
        smokeTorchParticles.Stop();
        torchLight.intensity = 1f;
        StartCoroutine(LightsOff());
        turnedOn = false;
        lightRadius.radius = 0.1f;
        animTorch.SetBool("isBurning", false);
    }

    void SetTorchLightOn()
    {
        smokeTorchParticles.Play();
        torchLight.intensity = 1f;
        StartCoroutine(LightsOn());
        turnedOn = true;
        lightRadius.radius = 2.8f;
        animTorch.SetBool("isBurning", true);
    }

    IEnumerator LightsOn()
    {
        Interpolator lightLerp = new Interpolator(0.2f, Interpolator.Type.SIN);
        lightLerp.ToMax();

        while (!lightLerp.isMaxPrecise)
        {
            lightLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            torchLight.pointLightOuterRadius = lightLerp.Value * outerRadiusOn;
            torchLight.pointLightInnerRadius = lightLerp.Value * innerRadiusOn;
            //WAIT A FRAME
            yield return null;
        }
    }

    IEnumerator LightsOff()
    {
        Interpolator lightLerp = new Interpolator(0.2f, Interpolator.Type.SIN);
        lightLerp.ToMax();

        while (!lightLerp.isMaxPrecise)
        {
            lightLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            torchLight.pointLightOuterRadius = lightLerp.Inverse * outerRadiusOn;
            torchLight.pointLightInnerRadius = lightLerp.Inverse * innerRadiusOn;
            //WAIT A FRAME
            yield return null;
        }
    }
}
