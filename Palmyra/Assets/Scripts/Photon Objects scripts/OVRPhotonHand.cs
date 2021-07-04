using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRPhotonHand : MonoBehaviour
{
    PhotonView pv;
    Transform target = null;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update() {
        if(target != null) {
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }
    }
}
