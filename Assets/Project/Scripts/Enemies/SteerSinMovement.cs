using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerSinMovement : SinMovement
{  
    Vector3 leftTargetDirection;
    Vector3 rightTargetDirection;
    LayerMask layerMask;
    RaycastHit2D leftHit;
    RaycastHit2D rightHit;
    Quaternion steerDirection;
    float distanceLeftHit;
    float distanceRightHit;
    float distanceDifference;
    float raycastDistance = 4.5f;

    Vector2 targetDirection;

    bool isCorrectingDirection = false;
    bool isWaitingToEndCorrection = false;
    float rotationAngle = 40f;
    float angleScaler;

    private void Awake()
    {
        Init();
        layerMask = LayerMask.GetMask("Default");
        steerDirection = new Quaternion();
    }



    public override void MoveTowardsTargetDirection(Vector2 targetDirection, float moveSpeed, float sinPercent = 1f)
    {
        if (isCorrectingDirection)
        {
            this.targetDirection = RaycastSteerCorrection(this.targetDirection);
        }
        else
        {
            this.targetDirection = RaycastSteerCorrection(targetDirection);
        }
        //Debug.DrawRay(transform.position, this.targetDirection * raycastDistance, Color.yellow);

        UpdateAngleDirection(this.targetDirection, sinPercent);

        if (isCorrectingDirection)
        {
            rigidbody.MovePosition((Vector2)transform.position + this.targetDirection * (moveSpeed * Time.deltaTime));
        }
        else
        {
            rigidbody.MovePosition((Vector2)transform.position + angleDirection + targetDirection * (moveSpeed * Time.deltaTime));
        }
    }

    public override void MoveTowardsTargetDirectionStraight(Vector2 targetDirection, float moveSpeed)
    {
        this.targetDirection = RaycastSteerCorrection(targetDirection);
        //Debug.DrawRay(transform.position, this.targetDirection * raycastDistance, Color.yellow);

        if (isCorrectingDirection&& IsHitClose()) return;

        rigidbody.MovePosition((Vector2)transform.position + targetDirection * (moveSpeed * Time.deltaTime));
    }



    private Vector2 RaycastSteerCorrection(Vector3 targetDirection)
    {
        leftTargetDirection = Quaternion.Euler(0, 0, 10f) * targetDirection * raycastDistance;
        rightTargetDirection = Quaternion.Euler(0, 0, -10f) * targetDirection * raycastDistance;

        leftHit = Physics2D.Raycast(transform.position, leftTargetDirection, raycastDistance, layerMask);
        rightHit = Physics2D.Raycast(transform.position, rightTargetDirection, raycastDistance, layerMask);

        //Debug.DrawRay(transform.position, targetDirection * raycastDistance, Color.white);
        //Debug.DrawRay(transform.position, leftTargetDirection, Color.green);
        //Debug.DrawRay(transform.position, rightTargetDirection, Color.red);

        if (leftHit.collider == null && rightHit.collider == null)
        {
            if (!isWaitingToEndCorrection) StartCoroutine(CorrectingDirectionToFalse());

            return targetDirection;
        }

        isCorrectingDirection = true;

        if (leftHit.collider != null && rightHit.collider == null)
        {
            angleScaler = 1;
        }
        else if (leftHit.collider == null && rightHit.collider != null)
        {
            angleScaler = -1;
        }
        else
        {
            distanceLeftHit = Vector2.Distance(leftHit.point, transform.position);
            distanceRightHit = Vector2.Distance(rightHit.point, transform.position);
            distanceDifference = distanceLeftHit - distanceRightHit;
            angleScaler = distanceDifference / raycastDistance;

            if (distanceDifference < 0.1f && distanceDifference > -0.1f)
            {
                angleScaler = 1;
            }
            else if (distanceDifference == 0)
            {
                angleScaler = 0;
            }

        }

        steerDirection = Quaternion.Euler(0, 0, rotationAngle * angleScaler);

        return steerDirection * targetDirection;
    }


    IEnumerator CorrectingDirectionToFalse()
    {
        isWaitingToEndCorrection = true;
        distanceLeftHit = distanceRightHit = 1f;

        yield return new WaitForSeconds(0.3f);

        if (leftHit.collider == null && rightHit.collider == null) isCorrectingDirection = false;
        isWaitingToEndCorrection = false;
    }


    private bool IsHitClose()
    {
        return distanceLeftHit < 1f || distanceRightHit < 1f;
    }


}
