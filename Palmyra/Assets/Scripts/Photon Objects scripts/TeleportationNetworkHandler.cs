using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TeleportationNetworkHandler : MonoBehaviour
{
    public Teleportation teleportObjects;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Activatecomponents()
    {

        teleportObjects.StartTeleportation();
    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }

    

}
