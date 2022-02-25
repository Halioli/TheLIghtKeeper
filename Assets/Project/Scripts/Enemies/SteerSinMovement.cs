using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerSinMovement : SinMovement
{
    //[SerializeField] PolygonCollider2D polygonColliderObject;
    //Vector2 colliderPointA; // top right point
    //Vector2 colliderPointB; // top left point

    //bool isInCollisionTrajectory;
    //float angle;
    //Vector2 colliderPosition;
    
    Vector3 leftTargetDirection;
    Vector3 rightTargetDirection;
    LayerMask layerMask;
    RaycastHit2D leftHit;
    RaycastHit2D rightHit;
    Quaternion steerDirection;
    float distanceLeftHit;
    float distanceRightHit;
    float distanceDifference;


    private void Awake()
    {
        Init();
        //colliderPointA = polygonColliderObject.GetComponent< PolygonCollider2D>().points[0];
        //colliderPointB = polygonColliderObject.GetComponent<PolygonCollider2D>().points[1];
        //colliderPointA = polygonColliderObject.points[0];
        //colliderPointB = polygonColliderObject.points[1];
        layerMask = LayerMask.NameToLayer("Default");
        steerDirection = new Quaternion();
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    colliderPosition = other.transform.position;
    //}
    
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    isInCollisionTrajectory = true;
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    isInCollisionTrajectory = false;
    //}



    public override void MoveTowardsTargetDirection(Vector2 targetDirection, float moveSpeed, float sinPercent = 1f)
    {
        //targetDirection = CorrectTargetDirection(targetDirection);
        targetDirection = RaycastSteerCorrection(targetDirection);

        UpdateAngleDirection(targetDirection, sinPercent);

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + targetDirection * (moveSpeed * Time.deltaTime));
    }


    //private Vector2 CorrectTargetDirection(Vector2 targetDirection)
    //{
        

    //    angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

    //    Quaternion rotation = Quaternion.Euler(0, 0, angle-90);
    //    polygonColliderObject.transform.localRotation = Quaternion.Slerp(polygonColliderObject.transform.localRotation, rotation, 50 * Time.fixedDeltaTime);


    //    return targetDirection;
    //}


    //private Vector2 GetRotatedPoint(Vector2 point)
    //{
    //    Vector2 rotatedPoint = new Vector2((point.x * Mathf.Cos(angle)) - (point.y * Mathf.Sin(angle)),
    //                                        (point.y * Mathf.Cos(angle)) - (point.x * Mathf.Sin(angle)));
    //    return rotatedPoint;
    //}

    //private bool IsAFurtherThanB()
    //{
    //    return Vector2.Distance(GetRotatedPoint(colliderPointA), colliderPosition) > Vector2.Distance(GetRotatedPoint(colliderPointB), colliderPosition);
    //}



    private Vector2 RaycastSteerCorrection(Vector3 targetDirection)
    {
        leftTargetDirection = Quaternion.Euler(0, 0, 10f) * targetDirection * 3f;
        rightTargetDirection = Quaternion.Euler(0, 0, -10f) * targetDirection * 3f;


        leftHit = Physics2D.Raycast(transform.position, leftTargetDirection, layerMask);
        rightHit = Physics2D.Raycast(transform.position, rightTargetDirection, layerMask);

        Debug.DrawRay(transform.position, leftTargetDirection, Color.green);
        Debug.DrawRay(transform.position, rightTargetDirection, Color.red);

        if (leftHit.collider == null && rightHit.collider == null)
        {
            Debug.Log(">>>>> NO HIT");
            return targetDirection;
        }


        if (leftHit.collider != null && rightHit.collider == null)
        {
            steerDirection = RotateLeftQuaternion();
            Debug.Log(">>>>> LEFT HIT");
        }
        else if (leftHit.collider == null && rightHit.collider != null)
        {
            steerDirection = RotateRightQuaternion();
            Debug.Log(">>>>> LEFT RIGHT");
        }
        else
        {
            distanceLeftHit = Vector2.Distance(leftHit.point, transform.position);
            distanceRightHit = Vector2.Distance(rightHit.point, transform.position);
            distanceDifference = distanceLeftHit - distanceRightHit;

            if (distanceDifference > 0.1f)
            {
                steerDirection = RotateLeftQuaternion();
            }
            else if (distanceDifference < -0.1f)
            {
                steerDirection = RotateRightQuaternion();
            }
            else
            {
                steerDirection = SuddenRotateQuaternion();
            }

        }
        return targetDirection;
        return steerDirection * targetDirection;
    }



    private Quaternion RotateLeftQuaternion()
    {
        return Quaternion.Euler(0, 0, 10);
    }

    private Quaternion RotateRightQuaternion()
    {
        return Quaternion.Euler(0, 0, -10);
    }

    private Quaternion SuddenRotateQuaternion()
    {
        return Quaternion.Euler(0, 0, 90);
    }

}
