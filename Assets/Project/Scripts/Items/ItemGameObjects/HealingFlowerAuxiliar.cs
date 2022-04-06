using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealingFlowerAuxiliar : MonoBehaviour
{
    [SerializeField] int healthToRefill = 3;
    private float punchTime = 0.5f;


    public delegate void HealthRefill(string description);
    public static event HealthRefill OnHealthRefill;

    private void Awake()
    {
        HealthSystem playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HealthSystem>();

        if (playerHealthSystem.GetMaxHealth() > playerHealthSystem.GetHealth())
        {
            playerHealthSystem.ReceiveHealth(healthToRefill);
        }
        StartCoroutine(Shake());

        if (OnHealthRefill != null) 
            OnHealthRefill("+" + healthToRefill.ToString());
    }

    IEnumerator Shake()
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.2f), punchTime);
        transform.DOShakeRotation(punchTime);
        yield return new WaitForSeconds(punchTime);
        Destroy(gameObject);
    }
}
