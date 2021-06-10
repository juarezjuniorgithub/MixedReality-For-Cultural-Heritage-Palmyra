using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{

    //@TODO Make code more readable, reduce redundancy of code
    [SerializeField] float floatYMaxValue = 0.7f;
    [SerializeField] float floatPosSpeed = 5;
    [SerializeField] float floatScaleSpeed = 5;
    [SerializeField] float floatRotateSpeed = 5;
    [SerializeField] float scaleFactor = 0.1f;
    Vector3 position;
    Vector3 initialPosition;
    Vector3 rotation;
    Vector3 initialRotation;

    bool startFloatSequence = false;
    bool startDeFloatSequence = false;

    bool gazeControlStatus = true;
    bool isFloating = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        position = initialPosition;

        rotation = new Vector3(0,1,0);
        initialRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);

    }

    void Update()
    {
        if(gazeControlStatus)
        {
            FloatSequence();
            DeFloatSequence();
            if(isFloating)
            {
                RotateObject();
            }
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

    void RotateObject()
    {
        transform.Rotate(rotation * Time.deltaTime * floatRotateSpeed);
    }

    public void StartFloatSequence()
    {
        startFloatSequence = true;
        startDeFloatSequence = false;
        isFloating = true;
    }

    public void StartDeFloatSequence()
    {
        startFloatSequence = false;
        startDeFloatSequence = true;
        isFloating = false;
        transform.localRotation = Quaternion.Euler(initialRotation);
    }

    void FloatSequence() //Inititaes Floating
    {
        float newYPos = position.y + (0.1f * Time.deltaTime * floatPosSpeed);
        if(startFloatSequence && !(newYPos > floatYMaxValue))
        {
            Vector3 newPos = new Vector3(position.x,newYPos,position.z);
            transform.localPosition = newPos;
            position.y = newYPos;
            Scale();
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
            DeScale();
        }
        else
        {
            startDeFloatSequence = false;
        }
    }

    void Scale()
    {
        float scaleValue = Time.deltaTime * scaleFactor * floatScaleSpeed;
        transform.localScale += new Vector3(scaleValue,scaleValue,scaleValue);
    }

    void DeScale()
    {
        float scaleValue = Time.deltaTime * scaleFactor * floatScaleSpeed;
        transform.localScale -= new Vector3(scaleValue,scaleValue,scaleValue);
    }

    public void DeactivateGazeControlSequence() //Deactivates the GazeControl
    {
        gazeControlStatus = false;
    }

    public void ActivateGazeControlSequence() //Activates the GazeControl
    {
        gazeControlStatus = true;
    }
    
}
