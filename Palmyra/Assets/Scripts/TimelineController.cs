using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    public EarthInteractionHandler earthInteractionHandler;
    public ExperimentalGrowAnimation experimentalGrowAnimation;
    
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
    }

    public void PlayGlobe()
    {
        //plays all playable directors attached to script
        playableDirectors[0].Play();    
    }

    public void PlayExpandSyria()
    {
        earthInteractionHandler.ActivateComponennts();
        earthInteractionHandler.ActivateComponnentsOnAllDevices();
        playableDirectors[1].Play();
        StartCoroutine(CallExperimentalGrow());
    }

    IEnumerator CallExperimentalGrow()
    {        
        yield return new WaitForSeconds(5);
        experimentalGrowAnimation.Grow();
    }
}
