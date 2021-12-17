using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class LightRodGameObject : ItemGameObject
{
    public AudioClip lightRodUseSound;
    public GameObject light;

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        GetComponent<SpriteRenderer>().renderingLayerMask = 1;
        StartCoroutine(Functionality());
    }


    private void FunctionalitySound()
    {
        audioSource.clip = lightRodUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }


    IEnumerator LightAppears()
    {
        float LIGHT_OUTER_RADIUS = light.GetComponent<Light2D>().pointLightOuterRadius;
        float LIGHT_RADIUS_DIFFERENCE = LIGHT_OUTER_RADIUS - light.GetComponent<Light2D>().pointLightInnerRadius;
        light.GetComponent<Light2D>().pointLightOuterRadius = 0;
        light.GetComponent<Light2D>().pointLightInnerRadius = 0;
        for (float i = 0f; i < LIGHT_OUTER_RADIUS; i += LIGHT_OUTER_RADIUS / 10f)
        {
            Debug.Log("wtffffff");
            light.GetComponent<Light2D>().pointLightOuterRadius = i;
            light.GetComponent<Light2D>().pointLightInnerRadius = i > LIGHT_RADIUS_DIFFERENCE ? i - LIGHT_RADIUS_DIFFERENCE : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }


    IEnumerator Functionality()
    {
        FunctionalitySound();

        Vector2 throwDirection = PlayerInputs.instance.GetMousePositionInWorld() - (Vector2)transform.position;
        throwDirection.Normalize();
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.AddForce(throwDirection * 4, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rigidbody2D.bodyType = RigidbodyType2D.Static;


        GameObject spawnedLight = Instantiate(light, transform);
        //StartCoroutine(LightAppears());

        float lightTime = 5f;
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        bool disappearing = false;

        while (lightTime > 0f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lightTime -= Time.deltaTime;

            if (lightTime <= 1f)
            {
                spawnedLight.GetComponent<Light2D>().intensity -= Time.deltaTime;
                spriteColor.r -= Time.deltaTime;
                spriteColor.g -= Time.deltaTime;
                spriteColor.b -= Time.deltaTime;
                GetComponent<SpriteRenderer>().color = spriteColor;
                if (!disappearing)
                {
                    StartDespawning(1);
                    disappearing = true;
                }
                
            }
        }

        Destroy(spawnedLight);
        Destroy(gameObject);
    }
}
