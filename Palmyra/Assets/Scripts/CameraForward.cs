using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForward : MonoBehaviour
{
    private Camera camera;

    private void Awake() {
        camera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.forward = camera.transform.forward;
    }
}
