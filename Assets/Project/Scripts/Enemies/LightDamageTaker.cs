using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamageTaker : MonoBehaviour
{
    [SerializeField] EnemyMonster enemyMonster;


    private void Awake()
    {
        enemyMonster = GetComponent<EnemyMonster>();
    }



    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Light") || otherCollider.CompareTag("LampLight"))
        {
            StartTakeDamageWhileInsideLight(); /////////////////////////////////////
        }
    }

    private void StartTakeDamageWhileInsideLight() {
        StartCoroutine(TakeDamageWhileInsideLight(0));
    }


    IEnumerator TakeDamageWhileInsideLight(int damageToTake)
    {

        while (false)
        {
            enemyMonster.ReceiveDamage(10);
        }
        yield return null;
    }



}
