using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EarthInteractionHandler : MonoBehaviour
{
    public GameObject syriaOnEarth;
    public GameObject syriaOnEarthToolTip;
    PhotonView photonView;
    //public TakeOverObject takeOverScript;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    [PunRPC]
    public void ActivateComponennts()
    {
        syriaOnEarth.SetActive(true);
        syriaOnEarthToolTip.SetActive(true);
        //takeOverScript.TakeOver();

        

    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("ActivateComponennts", RpcTarget.AllBuffered);
    }

}