using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeOverObject : MonoBehaviour
{
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
     
        
    }
    public void TakeOver() {
        pv.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

  

}
