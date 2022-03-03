using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SelectSpot : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Color visible;
    Color transparent;
    private bool active = false;
    private bool looping = false;

    private float punchTime = 0.25f;
    private int punchVibrato = 1;
    private float punchElasticity = 0.25f;



    private void Awake()
    {
        visible = new Color(255, 255, 255, 255);
        transparent = new Color(255, 255, 255, 0);


        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = transparent;
    }


    public void StopSelect()
    {
        StopCoroutine(Select());
        StopCoroutine(SelectLoop());

        spriteRenderer.color = transparent;
    }


    public void DoSelect()
    {
        if (active) return;

        StartCoroutine(Select());
    }

    IEnumerator Select()
    {
        spriteRenderer.color = visible;

        active = true;
        looping = false;
        transform.DOPunchScale(new Vector2(-0.2f, -0.2f), punchTime, punchVibrato, punchElasticity);
        yield return new WaitForSeconds(punchTime);

        active = false;
        spriteRenderer.color = transparent;
    }


    public void DoSelectLoop()
    {
        if (looping) return;
        looping = true;
        StartCoroutine(SelectLoop());
    }

    IEnumerator SelectLoop()
    {
        spriteRenderer.color = visible;

        while (looping)
        {
            transform.DOPunchScale(new Vector2(-0.2f, -0.2f), punchTime, punchVibrato, punchElasticity);
            yield return new WaitForSeconds(punchTime);
        }

        spriteRenderer.color = transparent;
    }

}
