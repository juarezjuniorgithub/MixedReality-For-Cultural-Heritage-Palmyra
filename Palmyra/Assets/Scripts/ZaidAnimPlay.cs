using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ZaidAnimPlay : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    public void PlayZaidAnimation()
    {
        playableDirector.Play();
    }
}
