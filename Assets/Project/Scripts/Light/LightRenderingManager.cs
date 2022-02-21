using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public class LightRenderingManager : MonoBehaviour
{
    Texture2D destinationTexture;
    [SerializeField] Camera camera;
    [SerializeField] Color32 shadowColor;

    bool updateMipMapsAutomatically;
    Rect regionToReadFrom;
    int xPosToWriteTo;
    int yPosToWriteTo;



    private void Awake()
    {
        updateMipMapsAutomatically = false;
        regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
    }


    private void OnEnable()
    {
        destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        camera = GetComponent<Camera>();
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }


    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (!camera == this.camera) return;

        xPosToWriteTo = 0;
        yPosToWriteTo = 0;

        // Copy the pixels from the Camera's render target to the texture
        destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);
    }



    Color32 GetColorInPixel(int pixelPositionX, int pixelPositionY)
    {
        return destinationTexture.GetPixel(pixelPositionX, pixelPositionY);
    }

    public bool IsPixelAShadowArea(int pixelPositionX, int pixelPositionY)
    {
        return AreTheSameColor(GetColorInPixel(pixelPositionX, pixelPositionY), shadowColor);
    }

    private bool AreTheSameColor(Color32 colorA, Color32 colorB)
    {
        return colorA.r == colorB.r && colorA.g == colorB.g && colorA.b == colorB.b;
    }


}
