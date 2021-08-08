using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] float delayToFade=0.05f;
    [SerializeField] float delaytoFadeIn=1f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] float fadeLimit = 1.0f;
    [SerializeField] List<Material> materials;

    bool fadeIn = false;
    bool fadeOut = false;

    void Start()
    {
        foreach(Material material in materials)
        {
            Color c = material.color;
            c.a = 0f;
            material.color = c;
        }
    }

    void Update()
    {
        if(fadeOut)
        {
            FadeOutAnimation();
        }
        if(fadeIn)
        {
            FadeInAnimation();
        }
    }

    public void ResetFadeValues()
    {
        foreach(Material material in materials)
        {
            Color c = material.color;
            c.a = 0f;
            material.color = c;
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
            foreach(Material material in materials)
            {
                Color c = material.color;
                c.a = f;
                material.color = c;
                yield return new WaitForSeconds(delayToFade);
            }   
        }
        
    }

    public void StartFadeInSequence()
    {
        fadeIn = true;
    }

    public void FadeInAnimation()
    {
        fadeIn = false;
        StartCoroutine(FadeInAnim());
    }

    IEnumerator FadeInAnim()
    {
        yield return new WaitForSeconds(delaytoFadeIn);
        
        for(float f = 0; f<=fadeLimit; f+=fadeStep)
        {
            foreach(Material material in materials)
            {
                Color c = material.color;
                c.a = f;
                material.color = c;
                yield return new WaitForSeconds(delayToFade);
            }
        }
    }
}
