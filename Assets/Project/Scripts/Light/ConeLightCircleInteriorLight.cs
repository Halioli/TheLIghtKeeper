using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeLightCircleInteriorLight : ConeLight
{
    [SerializeField] CircleCollider2D exteriorLightCollider;
    [SerializeField] CircleCollider2D interiorLightCollider;
    [SerializeField] float colliderDifference = 1f;


    private void Awake()
    {
        Init();
    }

    void Start()
    {
        SetColliderFitLightOuterRadius();
    }


    protected override void SetColliderFitLightOuterRadius()
    {
        base.SetColliderFitLightOuterRadius();

        exteriorLightCollider.radius = coneLight.pointLightOuterRadius;
        interiorLightCollider.radius = coneLight.pointLightOuterRadius - colliderDifference;
    }

}
