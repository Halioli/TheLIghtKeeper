using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeLightInteriorLight : ConeLight
{
    [SerializeField] PolygonCollider2D interiorLightCollider;
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

        float interiorSizeY = coneLight.pointLightOuterRadius - colliderDifference;
        float interiorSixeX = Mathf.Sin(coneLight.pointLightOuterAngle) * interiorSizeY;

        interiorLightCollider.points = new[] { new Vector2(interiorSixeX / 2, interiorSizeY), new Vector2(-interiorSixeX / 2, interiorSizeY), Vector2.zero };
    }


}
