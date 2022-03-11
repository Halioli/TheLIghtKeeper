using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomLight : MonoBehaviour
{
    protected enum LightState { NONE, SHRINKING, PARTIAL_SHRINKING, EXPANDING, EXTRA_EXPANDING };


    [SerializeField] protected GameObject lightGameObject;
    protected LightState lightState = LightState.NONE;
    protected bool active = false;
    public float expandTime;
    public float shrinkTime;
    [SerializeField] protected float extraExpandTime = 0.05f;
    [SerializeField] protected float partialShrinkTime = 0.05f;
    protected float lerpTransitionValue;
    protected float intensity;

    public virtual void Expand(float endIntensity, float endRadius = 0f) { }
    public virtual void Shrink(float endIntensity, float endRadius = 0f) { }
    public virtual void SetIntensity(float intensity) { }
    public virtual void SetDistance(float distance) { }
}
