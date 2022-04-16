using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShake : MonoBehaviour
{
    public float shakeIntensity = 3f;
    public float shakeTime = 1f;

    void Start()
    {
        DoDefaultShake();
    }

    public void DoDefaultShake()
    {
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
    }

    public void DoSpecificShake(float intensity, float time)
    {
        CinemachineShake.Instance.ShakeCamera(intensity, time);
    }
}
