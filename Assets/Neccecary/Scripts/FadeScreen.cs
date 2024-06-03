using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    static public float fadeDuration = 1;
    static public float startFadeDuration = 3;
    public Color fadeColor;
    public AnimationCurve fadeCurve;
    public string colorPropertyName = "_Color";
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        if (fadeOnStart)
            FadeOut(startFadeDuration);
    }

    public void FadeOut(float duration)
    {
        Fade(1, 0, duration);
    }
    
    public void FadeIn(float duration)
    {
        Fade(0, 1, duration);
    }

    public void Fade(float alphaIn, float alphaOut, float duration)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, duration));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, float duration)
    {
        rend.enabled = true;

        float timer = 0;
        while(timer <= duration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / duration));

            rend.material.SetColor(colorPropertyName, newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor(colorPropertyName, newColor2);

        if(alphaOut == 0)
            rend.enabled = false;
    }
}
