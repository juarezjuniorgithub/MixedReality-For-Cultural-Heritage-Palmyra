using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.UI;

public class SyriaNetworkHandler : MonoBehaviour
{
    public GameObject Syria;
    public PhotonView photonView;
    public GrowAnimation growAnimation;
    
    bool alreadyInteracted = false;

    
    void Start()
    {

    }
    [PunRPC]
    public void ActivateComponennts0()
    {
        Syria.SetActive(true);
        growAnimation.Grow();
        alreadyInteracted = true;


    }
    public void ActivateComponnentsOnAllDevices1()
    {
        if (alreadyInteracted==false)
        {
            photonView.RPC("ActivateComponennts0", RpcTarget.All);
        }
        
    }



}
