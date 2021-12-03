using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class ChangeColorLux : MonoBehaviour
{
    float duration;
    public Color[] colors;
    int currentColor = 0;
    private float colorTime = 0;

    private Light2D pointLight;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (colorTime >= duration)
        {
            colorTime = 0;
            currentColor = (currentColor + 1) % (colors.Length);
            duration = Random.Range(1.0f, 3.0f);
            Debug.Log(duration);
        }
        else
        {
            colorTime += Time.deltaTime;
            if(currentColor != 0)
            {
                pointLight.color = Color.Lerp(colors[(currentColor - 1) % (colors.Length)], colors[currentColor], colorTime);

            }
            else
            {
                pointLight.color = Color.Lerp(colors[colors.Length - 1], colors[currentColor], colorTime);

            }
        }
        //set light color
        //float t = Mathf.PingPong(Time.time, duration) / duration;
    }
}
