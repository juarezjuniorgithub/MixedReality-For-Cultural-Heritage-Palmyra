using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class ExperimentalHeadGaze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LogCurrentGazeTarget();
    }

    void LogCurrentGazeTarget()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            Debug.Log("User gaze is currently over game object: "
                + CoreServices.InputSystem.GazeProvider.GazeTarget);
                if(CoreServices.InputSystem.GazeProvider.GazeTarget.transform.tag == "monument")
                {
                    Debug.Log("monument");
                    CoreServices.InputSystem.GazeProvider.GazeTarget.GetComponent<GazeControl>().StartFloatSequence();
                }
        }

    }
}
