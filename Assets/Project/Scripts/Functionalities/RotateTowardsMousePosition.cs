using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMousePosition : MonoBehaviour
{
    public float rotationSpeed;

    private Vector2 direction;
    private Quaternion rotation;
    private float angle; 


    private void Start()
    {
        direction = new Vector2();
        rotation = new Quaternion();
    }


    void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (!PlayerInputs.instance.canMoveLantern) return;

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
