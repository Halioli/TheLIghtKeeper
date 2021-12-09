using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SelectSpot : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Select");
    }

    IEnumerator Select()
    {
        transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 1.5f, 4);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
