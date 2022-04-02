using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float farDistance = 10f;

    enum Follower { PLAYER, PILAR };
    Follower follower = Follower.PLAYER;

    Vector2 viewPortSize;
    Camera cam;

    public float viewPortFactor;

    Vector3 targetPosition;
    Vector3 followPosition;
    private Vector3 currentVelocity;
    public float followDuration;
    public float maximumFollowSpeed;

    public Transform player;
    [SerializeField] Transform pilarTransform;

    private Vector2 distance;
    private Vector3 cameraOffset = Vector3.forward * 10f;




    void Start()
    {
        cam = Camera.main;
        targetPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        viewPortSize = (cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - cam.ScreenToWorldPoint(Vector2.zero)) * viewPortFactor;

        distance = player.position - transform.position;

        if (Mathf.Abs(distance.x) > viewPortSize.x / 2)
        {
            targetPosition.x = player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x));
        }

        if (Mathf.Abs(distance.y) > viewPortSize.y / 2)
        {
            targetPosition.y = player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y));
        }


        // Target right follow
        if (follower == Follower.PLAYER)
        {
            targetPosition = player.position - cameraOffset;
        }
        else if (follower == Follower.PILAR)
        {
            targetPosition = pilarTransform.position - cameraOffset;
        }

        // Check if smooth or teleport follow
        if (IsFarFromTarget())
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followDuration, 400f);
            //transform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followDuration, maximumFollowSpeed);
        }

    }

    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;

        Gizmos.DrawCube(transform.position, viewPortSize);
    }


    private void OnEnable()
    {
        Torch.OnTorchStartActivation += SetPilarAsFollow;
        Torch.OnTorchEndActivation += SetPlayerAsFollow;
    }

    private void OnDisable()
    {
        Torch.OnTorchStartActivation -= SetPilarAsFollow;
        Torch.OnTorchEndActivation -= SetPlayerAsFollow;
    }


    private bool IsFarFromTarget()
    {
        return Vector2.Distance(transform.position, targetPosition) >= farDistance;
    }


    public void SetPlayerAsFollow()
    {
        follower = Follower.PLAYER;
    }

    public void SetPilarAsFollow()
    {
        follower = Follower.PILAR;
    }



}
