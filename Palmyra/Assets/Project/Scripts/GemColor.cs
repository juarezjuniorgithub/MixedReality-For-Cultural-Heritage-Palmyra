using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GemColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber==1)
        {
            //Set Color to red

        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            // Set Color Blue
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
