using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    private Interpolator lerp;
    private float lerpDistance = 0.3f;
    private float halfLerpDistance = 0.15f;
    private float startYLerp;


    void Awake()
    {
        lerp = new Interpolator(1f, Interpolator.Type.SMOOTH);
        startYLerp = transform.position.y;

        RandomFloatStart();
    }

    void Update()
    {
        ItemFloating();
    }


    private void ItemFloating()
    {
        lerp.Update(Time.deltaTime);
        if (lerp.isMinPrecise)
            lerp.ToMax();
        else if (lerp.isMaxPrecise)
            lerp.ToMin();

        transform.position = new Vector3(transform.position.x, startYLerp + (halfLerpDistance + lerpDistance * lerp.Value), 0f);
    }


    private void RandomFloatStart()
    {
        lerp.SetCurrentTime(Random.Range(0f, 1f));
    }

}
