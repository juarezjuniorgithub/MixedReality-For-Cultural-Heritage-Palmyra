using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusQuestNetworking : MonoBehaviour
{
    OVRCameraRig cameraRig = null;
    OvrAvatar avatar = null;
    [SerializeField] GameObject handR;
    [SerializeField] GameObject handL;

    // Update is called once per frame
    void Update()
    {
        if(cameraRig == null) {
            cameraRig = FindObjectOfType<OVRCameraRig>();
            if(cameraRig != null) {
                SetOculusQuestStuff();
            }
        }
        if (avatar == null) {
            avatar = FindObjectOfType<OvrAvatar>();
            if (avatar != null) {
                SetHands();
            }
        }
    }

    void SetOculusQuestStuff() {
        cameraRig.transform.position -= new Vector3(0, 1.5f, 0);
    }

    void SetHands() {
        PhotonNetwork.Instantiate(handR);
    }
}
