using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRodGameObject : ItemGameObject
{
    public AudioClip lightRodUseSound;
    public GameObject light;

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        StartCoroutine("Functionality");
    }


    private void UseSound()
    {
        audioSource.clip = lightRodUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    IEnumerator Functionality()
    {
        UseSound();

        GameObject spawnedLight = Instantiate(light, transform);

        float lightTime = 3f;

        while (lightTime > 0f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lightTime -= Time.deltaTime;
        }

        Destroy(spawnedLight);
        Destroy(gameObject);
    }
}
