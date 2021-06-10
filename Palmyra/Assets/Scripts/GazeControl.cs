using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{

    //@TODO Make code more readable, reduce redundancy of code
    [SerializeField] float floatYMaxValue = 0.7f;
    [SerializeField] float floatPosSpeed = 5;
    [SerializeField] float floatScaleSpeed = 5;
    [SerializeField] float scaleFactor = 0.1f;
    Vector3 position;
    Vector3 initialPosition;

    bool startFloatSequence = false;
    bool startDeFloatSequence = false;

    bool gazeControlStatus = true;

    void Start()
    {
        initialPosition = transform.localPosition;
        position = initialPosition;
    }

    void Update()
    {
        if(gazeControlStatus)
        {
            FloatSequence();
            DeFloatSequence();
        }
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartFloatSequence();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartDeFloatSequence();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            DeactivateGazeControlSequence();
        }
    }

    public void StartFloatSequence()
    {
        startFloatSequence = true;
        startDeFloatSequence = false;
    }

    public void StartDeFloatSequence()
    {
        startFloatSequence = false;
        startDeFloatSequence = true;
    }

    void FloatSequence() //Inititaes Floating
    {
        float newYPos = position.y + (0.1f * Time.deltaTime * floatPosSpeed);
        if(startFloatSequence && !(newYPos > floatYMaxValue))
        {
            Vector3 newPos = new Vector3(position.x,newYPos,position.z);
            transform.localPosition = newPos;
            position.y = newYPos;
        }
        else
        {
            startFloatSequence = false;
        }
    }

    void DeFloatSequence() //Initiates DeFloating
    {
        float newYPos = position.y - (0.1f * Time.deltaTime * floatPosSpeed);
        if(startDeFloatSequence && !(newYPos < initialPosition.y))
        {
            Vector3 newPos = new Vector3(position.x,newYPos,position.z);
            transform.localPosition = newPos;
            position.y = newYPos;
        }
        else
        {
            startDeFloatSequence = false;
        }
    }

    void DeactivateGazeControlSequence() //Deactivates the GazeControl
    {
        gazeControlStatus = false;
    }
    
}
