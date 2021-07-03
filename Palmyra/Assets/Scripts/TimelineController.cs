using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    public EarthInteractionHandler earthInteractionHandler;
    public ExperimentalGrowAnimation experimentalGrowAnimation;
    public DissolveEffect palmyraMapDissolveEffect;
    public List<GazeControl> mapObjects;

    bool animationExpandSyriaDone = false;

    void OnStart()
    {
        foreach(GazeControl mapObject in mapObjects)
        {
            mapObject.ResetAppearanceValueDissolve();
        }
    }
    
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
        StartCoroutine(ShowPalmyraMap());
    }

    public void PlayExpandSyria()
    {
        if(!animationExpandSyriaDone)
        {
            playableDirectors[1].Play();
            StartCoroutine(ShowEarthToolTip());
            StartCoroutine(CallExperimentalGrow());
            animationExpandSyriaDone = true;
        }
        else
        {
            playableDirectors[1].Play();
        } 
    }

    IEnumerator ShowEarthToolTip()
    {
        yield return new WaitForSeconds(6);
        earthInteractionHandler.ActivateComponennts();
        earthInteractionHandler.ActivateComponnentsOnAllDevices();
    }

    IEnumerator CallExperimentalGrow()
    {        
        yield return new WaitForSeconds(15);
        experimentalGrowAnimation.Grow();
    }

    IEnumerator ShowPalmyraMap()
    {        
        yield return new WaitForSeconds(4);
        palmyraMapDissolveEffect.InitiateAppearence();
        foreach(GazeControl mapObject in mapObjects)
        {
            mapObject.InitiateAppearanceOfObject();
        }
    }

}
