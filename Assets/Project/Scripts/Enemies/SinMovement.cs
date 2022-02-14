using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    // Sinusoidal movement
    [SerializeField] float amplitude = 0.1f;
    [SerializeField] float period = 0.1f;
    float theta;
    float sinWaveDistance;

    Vector2 directionTowardsTargetPosition;
    Vector2 angleDirection;
    Rigidbody2D rigidbody;

    float nearRadius;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        nearRadius = 0.25f;
    }


    public void MoveTowardsTargetPosition(Vector2 targetPosition, float moveSpeed)
    {
        MoveTowardsTargetDirection((targetPosition - rigidbody.position).normalized, moveSpeed);
    }

    public void MoveTowardsTargetDirection(Vector2 targetDirection, float moveSpeed)
    {
        // Sinusoidal movement
        theta = Time.timeSinceLevelLoad / period;
        sinWaveDistance = amplitude * Mathf.Sin(theta);

        directionTowardsTargetPosition = targetDirection;
        angleDirection = Vector2.Perpendicular(directionTowardsTargetPosition);
        angleDirection *= sinWaveDistance;

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + directionTowardsTargetPosition * (moveSpeed * Time.deltaTime));
    }

    public void MoveTowardsTargetDirectionStraight(Vector2 targetDirection, float moveSpeed)
    {
        directionTowardsTargetPosition = targetDirection;

        rigidbody.MovePosition((Vector2)transform.position + directionTowardsTargetPosition * (moveSpeed * Time.deltaTime));
    }

    public bool IsNearTargetPosition(Vector2 targetPosition)
    {
        return Vector2.Distance(targetPosition, transform.position) <= nearRadius;
    }



}
