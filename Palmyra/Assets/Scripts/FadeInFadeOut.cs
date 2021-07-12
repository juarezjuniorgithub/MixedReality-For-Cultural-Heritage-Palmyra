using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] float delayToFade=0.05f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] float fadeLimit = 1.0f;
    [SerializeField] float delayToFadeIn = 2.0f;
    [SerializeField] Material material;
    bool fadeIn = true;
    bool fadeOut = false;
    void Start()
    {
        Color c = material.color;
        c.a = 0f;
        material.color = c;
    }

    void Update()
    {
        if(fadeIn)
        {
            FadeIn();
        }
        if(fadeOut)
        {
            FadeOut();
        }
    }

    public void StartFadeInSequence()
    {
        fadeIn = true;
    }

    public void FadeIn()
    {
        fadeIn = false;
        StartCoroutine(FadeInAnim());
    }

    IEnumerator FadeInAnim()
    {
        for(float f = fadeStep; f<=fadeLimit; f+=fadeStep)
        {
            Color c = material.color;
            c.a = f;
            material.color = c;
            yield return new WaitForSeconds(delayToFade);
        }
        
        fadeOut = true;
    }

    public void FadeOut()
    {
        fadeOut = false;
        StartCoroutine(FadeOutAnim());
    }

    IEnumerator FadeOutAnim()
    {
        for(float f = fadeLimit; f>=-fadeStep; f-=fadeStep)
        {
            Color c = material.color;
            c.a = f;
            material.color = c;
            yield return new WaitForSeconds(delayToFade);
        }
        
        StartCoroutine(InitiateFadeIn());
    }

    IEnumerator InitiateFadeIn()
    {
        yield return new WaitForSeconds(delayToFadeIn);
        fadeIn = true;
    }
}
