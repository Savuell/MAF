using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Reference to the directional light in the scene
    public Light directionalLight;

    // Define the colors for day and night
    public Color dayColor = Color.white;
    public Color nightColor = Color.blue;

    // Define the intensity for day and night
    public float dayIntensity = 1f;
    public float nightIntensity = 0.2f;

    void Update()
    {
        // Get the current system time
        DateTime currentTime = DateTime.Now;
        int hour = currentTime.Hour;

        // Determine if it is day or night based on the hour
        bool isDayTime = hour >= 6 && hour < 18;

        // Change the light color and intensity based on the time of day
        if (isDayTime)
        {
            directionalLight.color = dayColor;
            directionalLight.intensity = dayIntensity;
        }
        else
        {
            directionalLight.color = nightColor;
            directionalLight.intensity = nightIntensity;
        }
    }

}
