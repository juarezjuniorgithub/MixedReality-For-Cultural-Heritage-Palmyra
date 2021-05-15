using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeOverObject : MonoBehaviour
{
    PhotonView pv;
    // Start is called before the first frame update
    void Awake()
    {
        pv = GetComponent<PhotonView>();
     
        
    }
    public void TakeOver() {
        pv.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

  

}
