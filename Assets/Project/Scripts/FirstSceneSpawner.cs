using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneSpawner : MonoBehaviour
{
    public GameObject meteor;

    public float targetTime;

    private void Awake()
    {
        targetTime = Random.Range(5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if(targetTime <= 0.0f)
        {
            Vector3 position = new Vector3(transform.position.x, Random.Range(transform.position.y - 10, transform.position.y), transform.position.z);
            Instantiate(meteor, position, Quaternion.identity);
            targetTime = Random.Range(5.0f, 10.0f);
        }
    }
}
