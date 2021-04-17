using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HandMenueNetworkHandler : MonoBehaviour
{
    PhotonView photonView;
    public GameObject HandMenue;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    [PunRPC]
    public void ActivateHandMenu()
    {
        HandMenue.SetActive(true);


        //takeOverScript.TakeOver();

        //photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");

    }
    public void ActivateComponnentsOnAllDevices1()
    {
        photonView.RPC("ActivateHandMenu", RpcTarget.All);


    }

    public void DeactivateHandMenu()
    {
        HandMenue.SetActive(false);


        //takeOverScript.TakeOver();

        //photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");

    }
    /*public void ActivateComponnentsOnAllDevices2()
    {
        photonView.RPC("DeactivateHandMenu", RpcTarget.All);
    }*/


}
