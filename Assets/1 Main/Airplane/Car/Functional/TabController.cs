using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Vector3[] values = new Vector3[4];
    [SerializeField] private TabColor[] tabs = new TabColor[4];
    [SerializeField] private GameObject[] sliders = new GameObject[4];
    [SerializeField] private Text[] texts = new Text[4];
    private string[] measures = new string[4];
    private float minSliderPos = 16.25f;
    private void Start()
    {
        for (int i = 0; i < 4; i++) 
        {
            measures[i] = texts[i].text;
        }
    }
    void Update()
    {
        SetTab();
    }
    void SetTab()
    {
        for (int i = 0; i < 4; i++)
        {
            float percent = (values[i].y - values[i].x) / (values[i].z - values[i].x);
            percent = Mathf.Max(Mathf.Min(percent, 1), 0);
            float r = Mathf.Min(1 - 2 * (percent - 0.5f), 1);
            float g = Mathf.Min(1 + 2 * (percent - 0.5f), 1);
            tabs[i].color = new Color(r, g, 0, 1);
            sliders[i].transform.localScale = new(percent, 1, 1);
            sliders[i].transform.localPosition = new((1 - percent) * minSliderPos, 0, 0);
            int floatSize = Mathf.Max(2 - (int)Mathf.Log10(values[i].y), 0);
            texts[i].text = values[i].y.ToString("F" + floatSize) + " " + measures[i];
        }
    }
}
