using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamageTaker : MonoBehaviour
{
    EnemyMonster enemyMonster;
    private bool isInsideLight;
    private bool isAlreadyTakingDamage;

    private int lightsCounter;
    private float waitTimeBeforeTakingDamage;
    private float damageTakeCooldown;

    int damageToTake;
    int lightDamage = 3;
    int intenseLightDamage = 7;



    private void Awake()
    {
        enemyMonster = GetComponent<EnemyMonster>();
        isInsideLight = false;
        isAlreadyTakingDamage = false;
        lightsCounter = 0;
        waitTimeBeforeTakingDamage = 0.25f;
        damageTakeCooldown = 0.3f;

        damageToTake = lightDamage;
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Light") || otherCollider.CompareTag("LampLight"))
        {
            ++lightsCounter;
            if (isInsideLight) return;

            isInsideLight = true;
            StartTakeDamageWhileInsideLight();
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Light") || otherCollider.CompareTag("LampLight"))
        {
            if (--lightsCounter == 0) isInsideLight = false;
        }
    }


    private void OnEnable()
    {
        LanternAttack.OnLanternAttackStart += () => damageToTake = intenseLightDamage;
        LanternAttack.OnLanternAttackEnd += () => damageToTake = lightDamage;
    }

    private void OnDisable()
    {
        LanternAttack.OnLanternAttackStart -= () => damageToTake = intenseLightDamage;
        LanternAttack.OnLanternAttackEnd -= () => damageToTake = lightDamage;
    }


    private void StartTakeDamageWhileInsideLight() {
        if (!isAlreadyTakingDamage)
        {
            StartCoroutine(TakeDamageWhileInsideLight());
        }
    }


    IEnumerator TakeDamageWhileInsideLight()
    {
        isAlreadyTakingDamage = true;
        yield return new WaitForSeconds(waitTimeBeforeTakingDamage);

        while (isInsideLight)
        {
            enemyMonster.ReceiveDamage(damageToTake);
            yield return new WaitForSeconds(damageTakeCooldown);
        }
        isAlreadyTakingDamage = false;
    }



}
