using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class Torch : InteractStation
{
    public ParticleSystem initialFireParticles;
    public Light2D torchLight;
    public CircleCollider2D lightRadiusCircle;

    private bool turnedOn = false;
    public TextMeshProUGUI interactText;

    public float torchIntensity = 1f;
    public float turnedOffIntensity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //initialFireParticles = GetComponentInChildren<ParticleSystem>();
        //torchLight = GetComponentInChildren<Light2D>();
        //lightRadiusCircle = GetComponentInChildren<CircleCollider2D>();
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
            StartCoroutine(PlayFireParticleSystem());
            torchLight.intensity = torchIntensity;
            //turnedOn = true;
        }
    }

    IEnumerator PlayFireParticleSystem()
    {
        initialFireParticles.Play();
        yield return new WaitForSeconds(1f);
        initialFireParticles.Stop();
    }
}
