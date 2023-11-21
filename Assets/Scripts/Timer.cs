using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float current;
    private float max;
    private bool countdown = false;

    public float Current { get { return current; } private set{} }



    public Timer(float max, bool countdown = false)
    {
        this.max = max;
        this.countdown = countdown;
        Reset();
    }

    // takes a float and adds it to the current time 
    // returns true if the timer has ended
    public bool Tick(float t)
    {
        if(countdown)
        {
            current -= t;
            return current <= 0f;
        }
        else 
        {
            current += t;
            return current >= max;
        }
    }

    public void Reset()
    {
        if(countdown) current = this.max;
        else current = 0f;
    }
}
