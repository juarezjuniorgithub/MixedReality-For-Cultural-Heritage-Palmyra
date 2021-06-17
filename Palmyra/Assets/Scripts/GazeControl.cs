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
    [SerializeField] GameObject buttons;

    [SerializeField] DissolveEffect dissolveEffect;
    Vector3 position;
    Vector3 initialPosition;
    Vector3 rotation;
    Vector3 initialRotation;
    Vector3 initialScale;

    bool startFloatSequence = false;
    bool startDeFloatSequence = false;

    bool gazeControlStatus = true;
    bool isFloating = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        position = initialPosition;

        initialScale = transform.localScale;

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
        if(gazeControlStatus)
        {
            startFloatSequence = true;
            startDeFloatSequence = false;
            isFloating = true;
        }

    }

    public void StartDeFloatSequence()
    {
        if(gazeControlStatus)
        {
            Debug.LogWarning("Called DefloatSequencce");
            StartCoroutine(CallDefloatSequence());
        }
    }

    IEnumerator CallDefloatSequence()
    {        
        yield return new WaitForSeconds(1.5f);
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
        if(transform.localScale.x >= initialScale.x)
            transform.localScale -= new Vector3(scaleValue,scaleValue,scaleValue);
    }

    public void DeactivateGazeControlSequence() //Deactivates the GazeControl
    {
        gazeControlStatus = false;
        buttons.SetActive(true);
    }

    public void ActivateGazeControlSequence() //Activates the GazeControl
    {
        gazeControlStatus = true;
        buttons.SetActive(false);
    }

    public void InitiateResetPlaygroundMap()
    {   

        dissolveEffect.InitiateDisappearence();
        StartCoroutine(ResetPlaygroundMap());
        
    }

    IEnumerator ResetPlaygroundMap()
    {
        yield return new WaitForSeconds(3);
        transform.localPosition = initialPosition;
        dissolveEffect.InitiateAppearence();
        transform.localRotation = Quaternion.Euler(initialRotation); 
        transform.localScale = initialScale;
        ActivateGazeControlSequence();
    }
    
}
