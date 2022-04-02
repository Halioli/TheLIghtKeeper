using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LanternFuelAuxiliar : MonoBehaviour
{
    [SerializeField] float lampTimeToRefill = 10f;
    private float punchTime = 0.5f;


    public delegate void LanternRefill(string description);
    public static event LanternRefill OnLanternRefill;

    private void Awake()
    {
        Lamp playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            playerLamp.RefillLampTime(lampTimeToRefill);
        }
        StartCoroutine(Shake());

        if (OnLanternRefill != null) OnLanternRefill("+"+ lampTimeToRefill.ToString());
    }

    IEnumerator Shake()
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.2f), punchTime);
        transform.DOShakeRotation(punchTime);
        yield return new WaitForSeconds(punchTime);
        Destroy(gameObject);
    }

}
