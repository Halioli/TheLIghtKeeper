using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternAttack : MonoBehaviour
{
    [SerializeField] Lamp lantern;
    private bool canAttack;
    [SerializeField] private float attackDuration;
    [SerializeField] private float cooldownDuration;
    private float lightAngleIncrement;
    private float lightDistanceIncrement;
    //private float lanternTimeConsumeValue;


    // Events
    public delegate void LanternAttackEvent();
    public static event LanternAttackEvent OnLanternAttackStart;
    public static event LanternAttackEvent OnLanternAttackEnd;



    private void Awake()
    {
        canAttack = true;

        lightAngleIncrement = 5.0f;
        lightDistanceIncrement = 3.5f;

        //lanternTimeConsumeValue = 5.0f;
    }

    private void Update()
    {
        //if (PlayerInputs.instance.PlayerClickedAttackButton() && IsAttackReady())
        //{
        //    StartLanternAttack();
        //}

        if (PlayerInputs.instance.IsAttackButtonDown() && IsAttackReady())
        {
            if (OnLanternAttackStart != null) OnLanternAttackStart();
            StartLanternAttack();
        }


    }


    private void OnEnable()
    {
        Lamp.turnOffLanternEvent += ForceQuitAttack;
    }

    private void OnDisable()
    {
        Lamp.turnOffLanternEvent -= ForceQuitAttack;
    }


    private bool IsAttackReady()
    {
        //return lantern.turnedOn && (lantern.lampTime > lanternTimeConsumeValue) && canAttack;
        return lantern.turnedOn && (lantern.lampTime > 0.1f) && canAttack;
    }


    private void StartLanternAttack()
    {
        StartCoroutine(ExpandLanternLight());
    }

    IEnumerator ExpandLanternLight()
    {
        canAttack = false;
        lantern.IncrementLightAngleAndDistance(lightAngleIncrement, lightDistanceIncrement);
        CinemachineShake.Instance.ShakeCamera(3f, 60f);
        
        //lantern.ConsumeLampTime(lanternTimeConsumeValue);
        //yield return new WaitForSeconds(attackDuration);
        yield return new WaitUntil(() => PlayerInputs.instance.IsAttackButtonUp());

        if (OnLanternAttackEnd != null) OnLanternAttackEnd();
        lantern.ResetLightAngleAndDistance(lightAngleIncrement, lightDistanceIncrement);
        CinemachineShake.Instance.ForceStopShakeCamera();
        yield return new WaitForSeconds(cooldownDuration);

        canAttack = true;
    }


    private void ForceQuitAttack()
    {
        if (canAttack) return;

        StopCoroutine(ExpandLanternLight());
        lantern.ResetLightAngleAndDistance(lightAngleIncrement, lightDistanceIncrement);
        canAttack = true;
    }


}
