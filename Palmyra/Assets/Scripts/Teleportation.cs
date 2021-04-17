using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Teleportation : MonoBehaviour
{
    public ParticleSystem effect1;
    public SkinnedMeshRenderer charBig;
    public ParticleSystem effect2;
    public SkinnedMeshRenderer charSmall;
    public PlayableDirector touringAnimation;
    public Material opaceMat;
    public Material fadeMat;
    public GameObject bigGlasses;
    public GameObject smallGasses;

    

    public void StartTeleportation()
    {
        StartCoroutine(TeleportationEffect());
        StartCoroutine(Fade());
    }

    IEnumerator TeleportationEffect()
    {
        effect1.Play();
        // wait/sleep method
        yield return new WaitForSeconds(0.5f);
        charBig.enabled = false;
        bigGlasses.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        effect2.Play();
        yield return new WaitForSeconds(0.5f);
        charSmall.enabled = true;
        smallGasses.SetActive(true);
        yield return new WaitForSeconds(3);
        touringAnimation.enabled = true;
   

    }
    IEnumerator Fade()
    {
        Color color = charBig.material.color;
        charBig.material = fadeMat;
        for (int i = 0; i < 100; i++)
        {
            color.a -= 0.01f;
            yield return new WaitForSeconds(0.001f);
        }

    }
}