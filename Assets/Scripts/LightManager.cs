using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LightManager : MonoBehaviour
{
   // Array to hold references to the lights you want to control
    public Light[] lights;

    void Update()
    {
        // Get the current system time
        DateTime currentTime = DateTime.Now;
        int hour = currentTime.Hour;

        // Check if the current time is between 18:00 and 6:00
        bool isNightTime = hour >= 18 || hour < 6;

        // Turn the lights on or off based on the time of day
        foreach (Light light in lights)
        {
            light.enabled = isNightTime;
        }
    }

}
