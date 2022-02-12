using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternAttack : MonoBehaviour
{
    [SerializeField] Lamp lantern;
    private bool canAttack;
    private float attackDuration;
    private float cooldownDuration;
    private float lightAngleIncrement;
    private float lanternLightIntensity;
    private float lightDistanceIncrement;
    private float lanternTimeConsumeValue;


    // Events
    public delegate void LanternAttackEvent();
    public static event LanternAttackEvent OnLanternAttackStart;
    public static event LanternAttackEvent OnLanternAttackEnd;



    private void Awake()
    {
        canAttack = true;

        attackDuration = 1.0f;
        cooldownDuration = 0.5f;

        lightAngleIncrement = 40.0f;
        lightDistanceIncrement = 3.5f;
        lanternLightIntensity = 2.0f;

        lanternTimeConsumeValue = 5.0f;
    }

    private void Update()
    {
        if (PlayerInputs.instance.PlayerClickedAttackButton() && lantern.turnedOn && canAttack)
        {
            StartLanternAttack();
        }
    }


    private void StartLanternAttack()
    {
        StartCoroutine(ExpandLanternLight());
    }

    IEnumerator ExpandLanternLight()
    {
        canAttack = false;
        lantern.IncrementLightAngleAndDistance(lightAngleIncrement, lightDistanceIncrement, lanternLightIntensity);
        lantern.ConsumeLampTime(lanternTimeConsumeValue);
        yield return new WaitForSeconds(attackDuration);

        lantern.ResetLightAngleAndDistance(lightAngleIncrement, lightDistanceIncrement);
        yield return new WaitForSeconds(cooldownDuration);

        canAttack = true;
    }


}
