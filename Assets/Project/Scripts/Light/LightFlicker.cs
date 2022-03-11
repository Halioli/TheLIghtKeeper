using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    const float LIGHT_INTENSITY_HIGH = 0.5f;
    const float LIGHT_INTENSITY_LOW = 0.25f;

    float flickCooldown = 1f;
    float flickerIntensity = 1f;
    float flickerTime = 0.08f;

    const int MIN_FLICK = 4;
    const int MAX_FLICK = 9;


    float distance = 11f;


    bool isFlicking;

    [SerializeField] private CircleLight circleLight;



    private void Start()
    {
        StartFlicking();
    }


    private void StartFlicking()
    {
        isFlicking = true;

        StartCoroutine(LightFlicking());
    }



    IEnumerator LightFlicking()
    {
        float lightingTime;
        int flickerCount;
        float flickingIntensity;
        float flickingTime;



        lightingTime = flickCooldown + Random.Range(0, 1); 

        while (isFlicking)
        {
            //circleLight.SetIntensity(LIGHT_INTENSITY_HIGH);

            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            //flickerCount = Random.Range(MIN_FLICK, MAX_FLICK);

            //yield return new WaitForSeconds(lightingTime);

            circleLight.Shrink(LIGHT_INTENSITY_LOW, 9f);
            yield return new WaitForSeconds(circleLight.shrinkTime);

            circleLight.Expand(LIGHT_INTENSITY_HIGH, 9f);
            yield return new WaitForSeconds(circleLight.expandTime);


            //interpolator.ToMax();
            //while (!interpolator.IsMax)
            //{
            //    interpolator.Update(Time.deltaTime);
            //    circleLight.SetIntensity(flickingIntensity);

            //    circleLight.

            //}

            //lightingTime = flickCooldown + Random.Range(0, 1);



            //for (int i = 0; i < flickerCount; ++i)
            //{
            //    flickingIntensity = Random.Range(LIGHT_INTENSITY_LOW, LIGHT_INTENSITY_HIGH);
            //    circleLight.SetIntensity(flickingIntensity);
            //    circleLight.SetDistance(distance - Random.Range(1f, 1.5f));

            //    flickingTime = Random.Range(0, 0.2f);// Random.Range(MIN_FLICK, MAX_FLICK) * flickerTime;
            //    //if (!isFlicking) break;
            //    yield return new WaitForSeconds(flickingTime);
            //}

        }

    }


}
