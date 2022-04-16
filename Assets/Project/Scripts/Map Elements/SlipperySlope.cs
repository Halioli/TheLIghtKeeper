using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperySlope : MonoBehaviour
{
    [SerializeField] AreaEffector2D areaEffector2d;
    [SerializeField] float slopeAngle;

    private PlayerInputs playerInputs;

    void Start()
    {
        areaEffector2d.forceAngle = slopeAngle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInputs = collision.GetComponent<PlayerInputs>();

            playerInputs.canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInputs = collision.GetComponent<PlayerInputs>();

            playerInputs.canMove = true;
        }
    }
}
