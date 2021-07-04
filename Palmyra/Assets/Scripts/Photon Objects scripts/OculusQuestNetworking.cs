using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusQuestNetworking : MonoBehaviour
{
    OVRCameraRig cameraRig = null;

    // Update is called once per frame
    void Update()
    {
        if(cameraRig == null) {
            cameraRig = FindObjectOfType<OVRCameraRig>();
            if(cameraRig != null) {
                SetOculusQuestStuff();
            }
        }
    }

    void SetOculusQuestStuff() {
        cameraRig.transform.position -= new Vector3(0, 1.5f, 0);
    }
}
