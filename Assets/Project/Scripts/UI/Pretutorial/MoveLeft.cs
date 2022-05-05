using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private Vector3 initialPos;
    [SerializeField] float velocity;
    [SerializeField] float sceneTime;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        while(sceneTime < 0)
        {
            sceneTime -= Time.deltaTime;
            initialPos.x -= Time.deltaTime * velocity;
            transform.position = initialPos;
        }
    }
}
