using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLightInteriorLight : CircleLight
{
    [SerializeField] CircleCollider2D interiorLightCollider;
    [SerializeField] float colliderDifference = 1f;



    private void Awake()
    {
        Init();
    }

    void Start()
    {
        SetColliderRadiusMatchLightOuterRadius();
    }


    protected override void SetColliderRadiusMatchLightOuterRadius()
    {
        base.SetColliderRadiusMatchLightOuterRadius();

        interiorLightCollider.radius = circleLight.pointLightOuterRadius - colliderDifference;
    }


}
