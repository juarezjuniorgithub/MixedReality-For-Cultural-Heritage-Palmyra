using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut: MonoBehaviour
{
    [SerializeField] float delayToFade=0.05f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] float fadeLimit = 1.0f;
    [SerializeField] Material material;
    bool fadeOut = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(fadeOut)
        {
            FadeOutAnimation();
        }
    }

    public void StartFadeOutSequence()
    {
        fadeOut = true;
    }

    public void FadeOutAnimation()
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
        
    }

}
