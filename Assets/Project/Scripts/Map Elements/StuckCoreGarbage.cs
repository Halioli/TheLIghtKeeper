using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class StuckCoreGarbage : MonoBehaviour
{
    [SerializeField] Transform[] garbageTransforms;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;
    bool isPlayingSound = false;

    float shakeForce = 16f;
    
    public void ShakeTransforms(float shakeDuration)
    {
        StartPlayShakeSound((shakeDuration));

        for (int i = 0; i < garbageTransforms.Length; ++i)
        {
            garbageTransforms[i].DOPunchRotation(new Vector3(0f, 0f, Random.Range(-shakeForce, shakeForce)), shakeDuration);
        }
    }


    private void StartPlayShakeSound(float shakeDuration)
    {
        if (isPlayingSound) return;

        StartCoroutine(PlayShakeSound(shakeDuration));
    }


    IEnumerator PlayShakeSound(float shakeDuration)
    {
        audioSource.Play();
        audioSource2.Play();
        isPlayingSound = true;

        yield return new WaitForSeconds(shakeDuration*2);
        
        audioSource.Pause();
        audioSource2.Pause();
        isPlayingSound = false;
    }


}
