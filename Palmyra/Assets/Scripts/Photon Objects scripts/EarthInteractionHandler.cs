using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.UI;

public class EarthInteractionHandler : MonoBehaviour
{
    public GameObject syriaOnEarth;
    public GameObject syriaOnEarthToolTip;
    PhotonView photonView;
    bool alreadyInteracted = false;
    //public Interactable earthInteractable;
    //public TakeOverObject takeOverScript;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    [PunRPC]
    public void ActivateComponennts()
    {
        syriaOnEarth.SetActive(true);
        syriaOnEarthToolTip.SetActive(true);
        alreadyInteracted = true;
        //earthInteractable.enabled = false;
        //takeOverScript.TakeOver();



    }

    public void ActivateComponnentsOnAllDevices()
    {
        if (alreadyInteracted==false)
        {
            photonView.RPC("ActivateComponennts", RpcTarget.AllBuffered);
        }
        
    }

}