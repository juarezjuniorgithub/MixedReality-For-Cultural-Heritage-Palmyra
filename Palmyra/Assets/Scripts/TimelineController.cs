using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    public EarthInteractionHandler earthInteractionHandler;
    public ExperimentalGrowAnimation experimentalGrowAnimation;

    bool animationExpandSyriaDone = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            PlayGlobe();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayExpandSyria();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayPalmyraMap();
        }
    }

    public void PlayGlobe()
    {
        //plays all playable directors attached to script
        playableDirectors[0].Play();    
    }

    public void PlayPalmyraMap()
    {
        playableDirectors[2].Play();
    }

    public void PlayExpandSyria()
    {
        if(!animationExpandSyriaDone)
        {
            earthInteractionHandler.ActivateComponennts();
            earthInteractionHandler.ActivateComponnentsOnAllDevices();
            playableDirectors[1].Play();
            StartCoroutine(CallExperimentalGrow());
            animationExpandSyriaDone = true;
        }
        else
        {
            playableDirectors[1].Play();
        } 
    }

    IEnumerator CallExperimentalGrow()
    {        
        yield return new WaitForSeconds(5);
        experimentalGrowAnimation.Grow();
    }
}
