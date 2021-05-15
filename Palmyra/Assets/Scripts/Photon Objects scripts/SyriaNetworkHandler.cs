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
    //public Interactable interactable;
    bool alreadyInteracted = false;

    
    void Start()
    {
        //photonView = GetComponent<PhotonView>();

    }
    [PunRPC]
    public void ActivateComponennts0()
    {
        Syria.SetActive(true);
        growAnimation.Grow();
        alreadyInteracted = true;
        //interactable.enabled = false;
        
        //takeOverScript.TakeOver();

        //photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");

    }
    public void ActivateComponnentsOnAllDevices1()
    {
        if (alreadyInteracted==false)
        {
            photonView.RPC("ActivateComponennts0", RpcTarget.All);
        }
        
    }



}
