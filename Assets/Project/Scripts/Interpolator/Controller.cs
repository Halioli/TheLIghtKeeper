using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    Interpolator lerp;

    public Image img;

    bool isFadeOut = false;
    // Start is called before the first frame update
    void Start()
    {
        lerp = new Interpolator(1f, Interpolator.Type.COS);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isFadeOut)
                StartCoroutine(FadeOut());
            else
                StartCoroutine(FadeIn());
        }
        lerp.Update(Time.deltaTime);
        if (lerp.isMinPrecise)
            lerp.ToMax();
        else if (lerp.isMaxPrecise)
            lerp.ToMin();

        transform.position = new Vector3(0f, 2f - 4f * lerp.Value, 0f);

    }

    IEnumerator FadeOut()
    {
        Interpolator fadeLerp = new Interpolator(0.5f, Interpolator.Type.SIN);
        fadeLerp.ToMax();

        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            img.color = new Color(0f, 0f, 0f, fadeLerp.Value);
            //WAIT A FRAME
            yield return null;
        }
        isFadeOut = true;
    }

    IEnumerator FadeIn()
    {
        Interpolator fadeLerp = new Interpolator(0.5f, Interpolator.Type.SIN);
        fadeLerp.ToMax();
        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            img.color = new Color(0f, 0f, 0f, fadeLerp.Inverse);
            //WAIT A FRAME
            yield return null;
        }
        isFadeOut = false;
    }
}
