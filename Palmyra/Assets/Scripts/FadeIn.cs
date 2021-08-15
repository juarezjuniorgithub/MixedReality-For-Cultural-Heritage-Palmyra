using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn: MonoBehaviour
{
    [SerializeField] float delayToFade=0.05f;
    [SerializeField] float delaytoActivate=1f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] float fadeLimit = 1.0f;
    [SerializeField] List<Material> materials;
    [SerializeField] List<GameObject> activateGameObjects;
    [SerializeField] List<GameObject> walls;
    bool fadeIn = false;
    
    void Start()
    {
        foreach(Material material in materials)
        {
            Color c = material.color;
            c.a = 0f;
            material.color = c;
        }
        
        foreach(GameObject gameObject in activateGameObjects)
        {
            gameObject.SetActive(false);
        }

        foreach(GameObject gameObject in walls)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(fadeIn)
        {
            FadeInAnimation();
        }
    }

    public void StartFadeInSequence()
    {
        fadeIn = true;
    }

    public void FadeInAnimation()
    {
        fadeIn = false;
         foreach(GameObject gameObject in walls)
        {
            gameObject.SetActive(true);
        }
        StartCoroutine(FadeInAnim());
    }

    IEnumerator FadeInAnim()
    {
        foreach(Material material in materials)
        {
            for(float f = fadeStep; f<=fadeLimit; f+=fadeStep)
            {
                Color c = material.color;
                c.a = f;
                material.color = c;
                yield return new WaitForSeconds(delayToFade);
            }
        }
        StartCoroutine(ActivateGameObjects());
    }

    IEnumerator ActivateGameObjects()
    {
        yield return new WaitForSeconds(delaytoActivate);
        foreach(GameObject gameObject in activateGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

}
