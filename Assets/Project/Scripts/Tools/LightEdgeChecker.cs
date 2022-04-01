using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEdgeChecker : MonoBehaviour
{
    [SerializeField] CircleCollider2D edgeCheckCollider;
    public bool isOutsideLight;


    void Update()
    {
        isOutsideLight = edgeCheckCollider.IsTouchingLayers(LayerMask.NameToLayer("Light"));
    }

}
