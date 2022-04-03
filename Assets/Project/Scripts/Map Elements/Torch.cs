using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class Torch : InteractStation
{
    public ParticleSystem smokeTorchParticles;
    public Light2D torchLight;
    public CanvasGroup popUpCanvasGroup;
    public CircleCollider2D lightRadius;
    public GameObject linkedRune;
    public GameObject desactivatedTorch;

    public float torchIntensity = 1f;
    public float turnedOffIntensity = 0f;
    public bool hasToBurn;

    private const float innerRadiusOn = 1.8f;
    private const float outerRadiusOn = 3.3f;
    public bool turnedOn = false;

    public Animator animTorch;

    public TorchPuzzleSystem puzzleSystem;

    [SerializeField] AudioSource torchAudioSource;


    public delegate void TorchPreAction(float duration);
    public static event TorchPreAction OnTorchPreStartActivation;
    public static event TorchPreAction OnTorchPreEndActivation;

    public delegate void TorchAction();
    public static event TorchAction OnTorchStartActivation;
    public static event TorchAction OnTorchEndActivation;





    void Awake()
    {
        SaveSystem.torches.Add(this);
    }

    void Start()
    {
        smokeTorchParticles.Stop();
        if (!turnedOn)
        {
            torchLight.pointLightOuterRadius = 0;
            torchLight.pointLightInnerRadius = 0;
            torchLight.intensity = turnedOffIntensity;
            lightRadius.radius = 0.1f;
        }

        popUpCanvasGroup.alpha = 0f;

        if(hasToBurn == false)
        {
            DoPuzzle();
        }
        PuzzleChecker();
        linkedRune.SetActive(false);

        desactivatedTorch.SetActive(true);
    }

    private void Update()
    {
        if (turnedOn) return;

        if (playerInsideTriggerArea)
        {
            popUpCanvasGroup.alpha = 1f;
            GetInput();
        }
        else
        {
            popUpCanvasGroup.alpha = 0f;
        }
    }
    public override void StationFunction()
    {
        if (!turnedOn)
        {
            SetTorchLightOn();
            torchAudioSource.Play();
        }
        else
        {
            SetTorchLightOff();
            torchAudioSource.Stop();
        }
        DoPuzzle();

    }
    public void SetTorchLightOff()
    {
        smokeTorchParticles.Stop();
        torchLight.intensity = 1f;
        //StartCoroutine(LightsOff());
        turnedOn = false;
        animTorch.SetBool("isBurning", false);
        if (hasToBurn)
        {
            linkedRune.SetActive(false);
        }
    }

    public void SetTorchLightOn()
    {
        smokeTorchParticles.Play();
        torchLight.intensity = 1f;
        //StartCoroutine(LightsOn());
        turnedOn = true;
        animTorch.SetBool("isBurning", true);
        if (hasToBurn)
        {
            linkedRune.SetActive(true);
        }
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
            lightRadius.radius = 2.8f;

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
            lightRadius.radius = 0.1f;
            //WAIT A FRAME
            yield return null;
        }
    }

    public void DoPuzzle()
    {
        if (turnedOn && hasToBurn)
        {
            puzzleSystem.torchesOn += 1;
            StartCoroutine(CameraTransitionToPilar());
        }
        else if (!turnedOn && hasToBurn)
        {
            puzzleSystem.torchesOn -= 1;
        }
        else if (turnedOn && !hasToBurn)
        {
            puzzleSystem.torchesOff -= 1;
        }
        else if (!turnedOn && !hasToBurn)
        {
            puzzleSystem.torchesOff += 1;
            StartCoroutine(CameraTransitionToPilar());
        }

    }

    private bool PuzzleChecker()
    {
        //Debug.Log("TORCHES ON: " + puzzleSystem.torchesOn);
        //Debug.Log("TORCHES OFF:" + puzzleSystem.torchesOff);
        if (puzzleSystem.torchesOn == puzzleSystem.maxTorchesOn && puzzleSystem.torchesOff == puzzleSystem.maxTorchesOff)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DesactivateTorchSprite()
    {
        desactivatedTorch.SetActive(false);
    }
    private void ActivateTorchSprite()
    {
        desactivatedTorch.SetActive(true);
    }


    IEnumerator CameraTransitionToPilar()
    {
        float pretime = 1.5f;
        float time = 3f;

        PlayerInputs.instance.canMove = false;
        PlayerInputs.instance.isLanternPaused = true;

        yield return new WaitForSeconds(1f);
        
        if (OnTorchPreStartActivation != null) OnTorchPreStartActivation(pretime);
        yield return new WaitForSeconds(pretime/2f);

        if (OnTorchStartActivation != null) OnTorchStartActivation();

        yield return new WaitForSeconds(time);

        if (PuzzleChecker())
        {
            //Debug.Log("Puzzle Completed");
            puzzleSystem.animator.SetBool("isCompleted", true);
        }

        yield return new WaitForSeconds(time);


        if (OnTorchPreEndActivation != null) OnTorchPreEndActivation(pretime);
        yield return new WaitForSeconds(pretime);
        

        PlayerInputs.instance.canMove = true;
        PlayerInputs.instance.isLanternPaused = false;
        if (OnTorchEndActivation != null) OnTorchEndActivation();

    }


}
