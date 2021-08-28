using Photon.Pun;
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
    public FadeIn map;
    public List<GazeControl> mapObjects;
    public List<FadeController> mapObjectsFadeController;
    public GameObject buttonPanel;

    bool animationExpandSyriaDone = false;

    PhotonView pv;
    bool isGlobeShowing;
    bool isMapShowing;

    private void Awake() {
        pv = GetComponent<PhotonView>();
        if (isGlobeShowing) {
            RPC_PlayExpandSyria();
        }
        if (isMapShowing) {
            RPC_PlayPalmyraMap();
        }
    }

    void Start()
    {
        foreach(GazeControl mapObject in mapObjects)
        {
            mapObject.ResetAppearanceValueDissolve();
        }

        foreach(FadeController mapObject in mapObjectsFadeController)
        {
            mapObject.ResetFadeValues();
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
        pv.RPC("RPC_PlayPalmyraMap", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_PlayPalmyraMap() {
        
        if(pv == null) {
            isMapShowing = true;
            return;
        }
        playableDirectors[2].Play();
        StartCoroutine(ShowPalmyraMap());
        buttonPanel.SetActive(false);
    }

    public void PlayExpandSyria()
    {
        pv.RPC("RPC_PlayExpandSyria", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_PlayExpandSyria() {
        if (pv == null) {
            isGlobeShowing = true;
            return;
        }
        if (!animationExpandSyriaDone) {
            playableDirectors[1].Play();
            StartCoroutine(ShowEarthToolTip());
            StartCoroutine(CallExperimentalGrow());
            animationExpandSyriaDone = true;
        } else {
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
        map.StartFadeInSequence();
        palmyraMapDissolveEffect.InitiateAppearence();
        
        foreach(GazeControl mapObject in mapObjects)
        {
            mapObject.InitiateAppearanceOfObject();
        }

        foreach(FadeController mapObject in mapObjectsFadeController)
        {
            mapObject.StartFadeInSequence();
        }
    }

}
