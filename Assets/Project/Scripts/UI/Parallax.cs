using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //private float lenght;
    //private float startPos;
    //public GameObject mainCam;
    //public float parallaxEffect;

    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;

    //public float targetTime = 40.0f;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    startPos = transform.position.x;
    //    lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    float temp = (mainCam.transform.position.x * (1 - parallaxEffect));
    //    float dist = (mainCam.transform.position.x * parallaxEffect);

    //    transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

    //    if (temp > startPos + lenght) startPos += lenght;
    //    else if (temp < startPos - lenght) startPos -= lenght;
    //}


    void Start()
    {
        startPosition = target = transform.position;
    }
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);
    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }
}
