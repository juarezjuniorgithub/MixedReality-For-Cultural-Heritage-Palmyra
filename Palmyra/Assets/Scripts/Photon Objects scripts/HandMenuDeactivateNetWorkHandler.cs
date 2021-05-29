using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandMenuDeactivateNetWorkHandler : MonoBehaviour
{
    PhotonView photonView;
    public GameObject HandMenue;
    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void DeactivateHandMenu()
    {
        HandMenue.SetActive(false);


        //takeOverScript.TakeOver();

        //photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");

    }
    public void ActivateComponnentsOnAllDevices5()
    {
        photonView.RPC("DeactivateHandMenu", RpcTarget.All);
    }
}
