using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamageTaker : MonoBehaviour
{
    EnemyMonster enemyMonster;
    private bool isInsideLight;
    private int lightsCounter;
    private float waitTimeBeforeTakingDamage;
    private float damageTakeCooldown;


    private void Awake()
    {
        enemyMonster = GetComponent<EnemyMonster>();
        isInsideLight = false;
        lightsCounter = 0;
        waitTimeBeforeTakingDamage = 0.25f;
        damageTakeCooldown = 2f;
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

    private void StartTakeDamageWhileInsideLight() {
        StartCoroutine(TakeDamageWhileInsideLight(5));
    }


    IEnumerator TakeDamageWhileInsideLight(int damageToTake)
    {
        yield return new WaitForSeconds(waitTimeBeforeTakingDamage);

        while (isInsideLight)
        {
            enemyMonster.ReceiveDamage(damageToTake);
            yield return new WaitForSeconds(damageTakeCooldown);
        }
    }



}
