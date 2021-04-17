using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandMenueNetorHandlerII : MonoBehaviour
{
    
    public GameObject HandMenue;
    PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    [PunRPC]
    public void DeactivateHandMenu()
    {
        HandMenue.SetActive(false);


    }

    public void ActivateComponnentsOnAllDevices2()
    {
        photonView.RPC("DeactivateHandMenu", RpcTarget.All);
    }



}

