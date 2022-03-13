using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LightRodAuxiliar : MonoBehaviour
{
    [SerializeField] float lightTime = 3f;
    private bool startedFading = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip lightRodUseSound;
    [SerializeField] CircleLight circleLight;


    private void Start()
    {
        StartCoroutine(Functionality());
    }


    private void StartSound()
    {
        audioSource.clip = lightRodUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    IEnumerator Functionality()
    {
        StartSound();

        circleLight.Expand(1f);
        transform.DOShakeRotation(lightTime/2, 40, 5, 40);


        while (lightTime > 0f)
        {
            yield return null;
            lightTime -= Time.deltaTime;

            if (lightTime <= 1f)
            {
                if (!startedFading)
                {
                    startedFading = true;
                    transform.DOShakeRotation(1f);
                    FadeSound();
                }
                circleLight.SetIntensity(circleLight.intensity - Time.deltaTime);

            }
        }

        
        circleLight.SetLightActive(false);
        yield return new WaitForSeconds(0.15f);


        Destroy(gameObject);
    }

    private void FadeSound()
    {
        startedFading = true;
        audioSource.clip = lightRodUseSound;
        audioSource.pitch = Random.Range(1.4f, 1.5f);
        audioSource.Play();
    }



}
