using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LanternFuelAuxiliar : MonoBehaviour
{
    [SerializeField] float lampTimeToRefill = 10f;
    private float punchTime = 0.5f;


    private void Awake()
    {
        Lamp playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            playerLamp.RefillLampTime(lampTimeToRefill);
        }
        StartCoroutine(Shake());

    }

    IEnumerator Shake()
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.2f), punchTime);
        transform.DOShakeRotation(punchTime);
        yield return new WaitForSeconds(punchTime);
        Destroy(gameObject);
    }

}
