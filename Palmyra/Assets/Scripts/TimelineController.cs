using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Play();
        }
    }

    public void Play()
    {
        //plays all playable directors attached to script
        foreach(PlayableDirector playableDirector in playableDirectors)
        {
            playableDirector.Play();    
        }
    }
}
