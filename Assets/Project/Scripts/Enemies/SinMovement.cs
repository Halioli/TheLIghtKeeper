using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    // Sinusoidal movement
    [SerializeField] protected float amplitude = 0.1f;
    [SerializeField] protected float period = 0.1f;
    float theta;
    float sinWaveDistance;

    protected Vector2 angleDirection;
    protected Rigidbody2D rigidbody;

    float nearRadius;


    private void Awake()
    {
        Init();
    }

    protected void Init()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        nearRadius = 0.25f;
    }


    public void MoveTowardsTargetPosition(Vector2 targetPosition, float moveSpeed, float sinPercent = 1f)
    {
        MoveTowardsTargetDirection((targetPosition - rigidbody.position).normalized, moveSpeed, sinPercent);
    }

    public virtual void MoveTowardsTargetDirection(Vector2 targetDirection, float moveSpeed, float sinPercent = 1f)
    {
        UpdateAngleDirection(targetDirection, sinPercent);

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + targetDirection * (moveSpeed * Time.deltaTime));
    }

    protected void UpdateAngleDirection(Vector2 targetDirection, float sinPercent)
    {
        // Sinusoidal movement
        theta = Time.timeSinceLevelLoad / period;
        sinWaveDistance = amplitude * Mathf.Sin(theta);

        angleDirection = Vector2.Perpendicular(targetDirection);
        angleDirection *= sinWaveDistance;
        angleDirection *= sinPercent;
    }



    public virtual void MoveTowardsTargetDirectionStraight(Vector2 targetDirection, float moveSpeed)
    {
        rigidbody.MovePosition((Vector2)transform.position + targetDirection * (moveSpeed * Time.deltaTime));
    }



    public bool IsNearTargetPosition(Vector2 targetPosition)
    {
        return Vector2.Distance(targetPosition, transform.position) <= nearRadius;
    }



}
