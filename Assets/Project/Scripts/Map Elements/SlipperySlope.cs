using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperySlope : MonoBehaviour
{
    [SerializeField] AreaEffector2D areaEffector2d;
    [SerializeField] float slopeAngle;

    // Start is called before the first frame update
    void Start()
    {
        areaEffector2d.forceAngle = slopeAngle;
    }
}
