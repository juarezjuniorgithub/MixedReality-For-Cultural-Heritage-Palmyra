using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayParticale : MonoBehaviour
{
    public ParticleSystem Effect;
    // This fuction starts other funktion as soon you are in play mode
    private void Start()
    {
       
        
        StartCoroutine(playEffectAfterTime());

        

    }

    IEnumerator playEffectAfterTime()
    {
        yield return new WaitForSeconds(20);
        Effect.Play();

    }
}