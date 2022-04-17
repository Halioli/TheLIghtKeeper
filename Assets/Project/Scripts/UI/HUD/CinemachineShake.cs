using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    //Private Atributes
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;    
    private float startingIntensity;
    //private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    //Public Atributes
    public static CinemachineShake Instance { get; private set; }
    //[SerializeField] CinemachineVirtualCamera cinemachineVirtual;

    [SerializeField] private CinemachineVirtualCamera vcam = null;
    [System.NonSerialized] private CinemachineBasicMultiChannelPerlin noiseComp = null;

    public delegate void CinemachineShakeAction();
    public static event CinemachineShakeAction OnShakeStop;

    private void Awake()
    {
        noiseComp = vcam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if (noiseComp == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noiseComp}");

        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        //cinemachineBasicMultiChannelPerlin =
        //    cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        PauseMenu.OnPaused += ForceStopShakeCamera;

    }

    private void OnDisable()
    {
        PauseMenu.OnPaused -= ForceStopShakeCamera;
    }


    public void ShakeCamera(float intensity, float time)
    {

        startingIntensity = intensity;
        noiseComp.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    public void ForceStopShakeCamera()
    {
        shakeTimer = Time.deltaTime;
        if(OnShakeStop != null) OnShakeStop();
    }


    // Update is called once per frame
    void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            //CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            //    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noiseComp.m_AmplitudeGain = 
                Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
        else
        {
            ForceStopShakeCamera();
        }
    }
}
