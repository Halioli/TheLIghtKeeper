using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomLight : MonoBehaviour
{
    protected enum LightState { NONE, SHRINKING, PARTIAL_SHRINKING, EXPANDING, EXTRA_EXPANDING };


    [SerializeField] protected GameObject lightGameObject;
    protected LightState lightState = LightState.NONE;
    protected bool active = false;
    [SerializeField] protected float expandTime;
    [SerializeField] protected float shrinkTime;
    protected float lerpTransitionValue;
    protected float intensity;

    public virtual void Expand(float endIntensity) { }
    public virtual void Shrink(float endIntensity) { }
    public virtual void SetIntensity(float intensity) { }
    public virtual void SetDistance(float distance) { }
}
