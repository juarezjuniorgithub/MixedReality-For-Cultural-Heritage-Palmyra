using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class ExperimentalHeadGaze : MonoBehaviour
{
    
    void Start()
    {
        PointerUtils.SetGazePointerBehavior(PointerBehavior.AlwaysOn); //makes sure that the head gaze is always on
    }

    void Update()
    {
        //LogCurrentGazeTarget();
    }


    public void StartHeadGazePointer()
    {
        PointerUtils.SetGazePointerBehavior(PointerBehavior.AlwaysOn); //makes sure that the head gaze is always on
    }

    public void StopHeadGazePointer()
    {
        PointerUtils.SetGazePointerBehavior(PointerBehavior.AlwaysOff); //makes sure that the head gaze is always off
    }
    
    
    
    
    //Ignore
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
        Debug.Log(CoreServices.InputSystem.GazeProvider.HitInfo);

    }
}
