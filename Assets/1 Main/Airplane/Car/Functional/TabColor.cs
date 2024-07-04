using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabColor : MonoBehaviour
{
    public Color color;
    [SerializeField] private RawImage[] images = new RawImage[3];
    [SerializeField] private Text text;
    void Update()
    {
        foreach(RawImage image in images)image.color = color;
        text.color = color;
    }
}
