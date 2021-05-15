using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.UI;

public class NarrtorController : MonoBehaviour
{
    public GameObject bigZaidZaim;
    //public GameObject narratorButton;
    [SerializeField]
    private PressableButtonHoloLens2 pressableButtonHoloLens2;
    
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
        if (bigZaidZaim.activeSelf)
        {
            pressableButtonHoloLens2.enabled = false;

        }
        
    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }
}
