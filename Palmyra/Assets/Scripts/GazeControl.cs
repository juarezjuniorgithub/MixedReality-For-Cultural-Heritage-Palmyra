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
    [SerializeField] float delayForReset = 3f;

    [Tooltip("Delay for buttons and objects to become active")]
    [SerializeField] float additionalObjectsActivationDelay = 0.5f;
    [SerializeField] float activateOtherAdditionalObjectsDelay = 1f;
    [SerializeField] float  holderDeactivationDelay = 1f;
    [SerializeField] GameObject buttons;

    [SerializeField] List<GameObject> additionalActivators;
    [Tooltip("For Holder GameObjects")]
    [SerializeField] List<GameObject> holderDeactivators;

    [Tooltip("Keep 0th element as the dissolve effect on the map initiation and elements from 1th position onwards to dissolve in during manipulation")]
    [SerializeField] List<DissolveEffect> dissolveEffect;
    Vector3 position;
    Vector3 initialPosition;
    Vector3 rotation;
    Vector3 initialRotation;
    Vector3 initialScale;

    [SerializeField] bool doNotDeactivateFirstDissolveEffect = false;
    
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

    public void DeactivateOnlyGazeControl()
    {
        gazeControlStatus = false;
    }

    public void DeactivateGazeControlSequence() //Deactivates the GazeControl
    {
        StartCoroutine(AcitvateAdditionalGameObjects());
    }

    IEnumerator AcitvateAdditionalGameObjects()
    {
        yield return new WaitForSeconds(additionalObjectsActivationDelay);
        buttons.SetActive(true);
    }

    public void ActivateAllAdditionalObjects()
    {
        if(!doNotDeactivateFirstDissolveEffect)
        {
            dissolveEffect[0].InitiateDisappearence();
        }
        StartCoroutine(ActivateHighPolyModels());
    }

    IEnumerator ActivateHighPolyModels()
    {
        yield return new WaitForSeconds(activateOtherAdditionalObjectsDelay);
        foreach(GameObject gameObject in additionalActivators)
        {
            gameObject.SetActive(true);
        }


        for(int i=1; i<dissolveEffect.Count; i++)
        {
            dissolveEffect[i].InitiateAppearence();  //fade in all other secondary items with dissolve shaders
        }

        StartCoroutine(DeactivateHolders());
    }

    IEnumerator DeactivateHolders()
    {
        yield return new WaitForSeconds(holderDeactivationDelay);
        foreach(GameObject gameObject in holderDeactivators)
        {
            gameObject.SetActive(false);
        }
    }

    public void ActivateGazeControlSequence() //Activates the GazeControl
    {
        gazeControlStatus = true;

        for(int i=1; i<dissolveEffect.Count; i++)
        {
            dissolveEffect[i].InitiateDisappearence(); //fade out all other secondary items with dissolve shaders
        }

        foreach(GameObject gameObject in additionalActivators)
        {
            gameObject.SetActive(false);
        }
        buttons.SetActive(false);
    }

    public void InitiateResetPlaygroundMap()
    {   
        foreach(GameObject gameObject in holderDeactivators)
        {
            gameObject.SetActive(true);
        }

        for(int i=0; i<dissolveEffect.Count; i++)
        {
            dissolveEffect[i].InitiateDisappearence(); 
        }

        StartCoroutine(ResetPlaygroundMap());
        
    }

    IEnumerator ResetPlaygroundMap()
    {
        yield return new WaitForSeconds(delayForReset);
        transform.localPosition = initialPosition;
        dissolveEffect[0].InitiateAppearence();
        transform.localRotation = Quaternion.Euler(initialRotation); 
        transform.localScale = initialScale;
        ActivateGazeControlSequence();
    }

    public void InitiateAppearanceOfObject()
    {
        StartCoroutine(AppearObject());
    }

    IEnumerator AppearObject()
    {
        yield return new WaitForSeconds(3);
        transform.localPosition = initialPosition;
        dissolveEffect[0].InitiateAppearence();
        transform.localRotation = Quaternion.Euler(initialRotation); 
        transform.localScale = initialScale;
        ActivateGazeControlSequence();
    }

    public void ResetAppearanceValueDissolve()
    {
            dissolveEffect[0].ResetAppearanceValue();
    }
    
}
