using System.Collections;
using System.Collections.Generic;
using System;
public class Interpolator
{

    public enum State { MIN, MAX, SHRINKING, GROWING };  //States for interpolation
    public enum Type { LINEAR, SIN, COS, QUADRATIC, SMOOTH }; //Types of Interpolation

    public State m_interpolationState = State.MIN;  //Initialization of state
    public Type m_interpolationType;

    private float m_currentTime = 0.0f;
    private float m_Interpolationtime;

    public float Value { get; private set; } = 0.0f;
    public float Inverse { get { return 1f - Value; } }

    private readonly float m_epsilon = 0.05f;

    public bool isMaxPrecise { get { return m_interpolationState == State.MAX; } }
    public bool isMinPrecise { get { return m_interpolationState == State.MIN; } }

    public bool IsMax { get { return Value > 1f - m_epsilon; } }
    public bool IsMin { get { return Value < m_epsilon; } }

    public Interpolator(float interpolationTime, Type interpolationType = Type.LINEAR)
    {
        m_Interpolationtime = interpolationTime;
        m_interpolationType = interpolationType;
    }

    public void ToMax()
    {
        m_interpolationState = m_interpolationState != State.MAX ? State.GROWING : State.MAX;
    }
    public void ToMin()
    {
        m_interpolationState = m_interpolationState != State.MIN ? State.SHRINKING : State.MIN;
    }


    public void ForceMax()
    {
        m_currentTime = m_Interpolationtime;
        Value = 1f;
        m_interpolationState = State.MAX;
    }

    public void ForceMin()
    {
        m_currentTime = 0.0f;
        Value = 0f;
        m_interpolationState = State.MIN;
    }

    public void Update(float dt)
    {
        if(m_interpolationState == State.MIN || m_interpolationState == State.MAX)
        {
            return;
        }
        float modifieDT = m_interpolationState == State.GROWING ? dt : -dt;
        m_currentTime += modifieDT;

        if(m_currentTime >= m_Interpolationtime)
        {
            ForceMax();
            return;
        }
        else if(m_currentTime <= 0.0f)
        {
            ForceMin();
            return;
        }

        Value = m_currentTime / m_Interpolationtime;

        switch (m_interpolationType)
        {
            case Type.SIN:
                Value = (float)Math.Sin(Value * Math.PI * 0.5f);
                break;
            case Type.COS:
                Value = 1F-(float)Math.Cos(Value * Math.PI * 0.5f);
                break;
            case Type.QUADRATIC:
                Value *= Value;
                break;
            case Type.SMOOTH:
                Value = Value * Value * (3f - 2f * Value);
                break;
            default:
                break;
        }
    }

}
