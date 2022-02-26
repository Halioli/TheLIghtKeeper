using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerSinMovement : SinMovement
{
    [SerializeField] PolygonCollider2D polygonColliderObject;
    Vector2 colliderPointA; // top right point
    Vector2 colliderPointB; // top left point

    bool isInCollisionTrajectory;


    private void Awake()
    {
        Init();
        //colliderPointA = polygonColliderObject.GetComponent< PolygonCollider2D>().points[0];
        //colliderPointB = polygonColliderObject.GetComponent<PolygonCollider2D>().points[1];
        colliderPointA = polygonColliderObject.points[0];
        colliderPointB = polygonColliderObject.points[1];

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        isInCollisionTrajectory = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInCollisionTrajectory = false;
    }



    public override void MoveTowardsTargetDirection(Vector2 targetDirection, float moveSpeed, float sinPercent = 1f)
    {
        targetDirection = CorrectTargetDirection(targetDirection);

        UpdateAngleDirection(targetDirection, sinPercent);

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + targetDirection * (moveSpeed * Time.deltaTime));
    }


    private Vector2 CorrectTargetDirection(Vector2 targetDirection)
    {
        if (isInCollisionTrajectory)
        {
            targetDirection += Vector2.Perpendicular(targetDirection);
            targetDirection.Normalize();
        }

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle-90);
        polygonColliderObject.transform.localRotation = Quaternion.Slerp(polygonColliderObject.transform.localRotation, rotation, 50 * Time.fixedDeltaTime);


        return targetDirection;
    }


    private Vector2 GetRotatedPoint(Vector2 point, float angle)
    {
        Vector2 rotatedPoint = new Vector2((point.x * Mathf.Cos(angle)) - (point.y * Mathf.Sin(angle)),
                                            (point.y * Mathf.Cos(angle)) - (point.x * Mathf.Sin(angle)));
        return rotatedPoint;
    }





}
