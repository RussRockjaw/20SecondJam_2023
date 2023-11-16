using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float current;
    public float Current { get { return current; } private set{} }
    private float max;


    public Timer(float max)
    {
        this.max = max;
        current = 0.0f;
    }

    // takes a float and adds it to the current time 
    // returns true if the timer has reached its max
    public bool Tick(float t)
    {
        current += t;
        return current >= max;
    }

    public void Reset()
    {
        current = 0.0f;
    }
}
