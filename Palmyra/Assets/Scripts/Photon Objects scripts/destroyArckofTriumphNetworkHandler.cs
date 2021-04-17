using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroyArckofTriumphNetworkHandler : MonoBehaviour
{
    PhotonView photonView;
    public DestroyMonument destroyMonument;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Activatecomponents()
    {

        destroyMonument.Destroy();
    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }


}
