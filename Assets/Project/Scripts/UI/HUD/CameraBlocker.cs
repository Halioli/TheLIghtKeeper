using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlocker : MonoBehaviour
{
    Vector3 position = new Vector3(0, 0, -10);


    void Update()
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
}
