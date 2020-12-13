using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BuildArckNetworkHandler : MonoBehaviour
{
    PhotonView photonView;
    public DestroyMonument buildMonument;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void Activatecomponents()
    {

        buildMonument.Rebuild();
    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }

}
