using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class QuestionmarkNetworkHandler : MonoBehaviour
{
    public GameObject bigZaidZaim;
    public GameObject questionmark;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    [PunRPC]
    public void Activatecomponents()
    {
        bigZaidZaim.SetActive(true);
        questionmark.SetActive(false);
    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }
}
