using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{
    //on head gaze focus by user how high should the monument go in the Y axis.
    [SerializeField] float floatYMaxValue = 0.7f;
    //on head gaze focus by user how fast should the monument go up in the Y axis
    [SerializeField] float floatPosSpeed = 5;
    //on head gaze focus by user how fast should the monument scale
    [SerializeField] float floatScaleSpeed = 5;
    //on head gaze focus by user how fast should the monument rotate
    [SerializeField] float floatRotateSpeed = 5;
    //what is the scale factor that the monument is being scaled to
    [SerializeField] float scaleFactor = 0.1f;
    //what is the delay before the monuments are reset to its original position
    [SerializeField] float delayForReset = 3f;

    //what is the delays before buttons are set active
    [Tooltip("Delay for buttons and objects to become active")]
    [SerializeField] float buttonObjectsActivationDelay = 0.5f;
    // what is the delay before the additional objects are activated
    [SerializeField] float activateOtherAdditionalObjectsDelay = 1f;
    //what is the delay before holders are deactivated
    [SerializeField] float  holderDeactivationDelay = 1f;
    // reference to the button gameObject
    [SerializeField] GameObject buttons;
    //List of additional gameObjects that need to be set active later
    [SerializeField] List<GameObject> additionalActivators;
    //for holder GameObjects that needs to be deactivated when additional activators are activated. 
    //Eg the lowpoly beauty holder in the scene
    [Tooltip("For Holder GameObjects")]
    [SerializeField] List<GameObject> holderDeactivators;

    //List of dissolveEffect scripts that needs to be called
    [Tooltip("Keep 0th element as the dissolve effect on the map initiation and elements from 1th position onwards to dissolve in during manipulation")]
    [SerializeField] List<DissolveEffect> dissolveEffect; 
    //List of FadeController scripts for the monument that needs to be called
    [SerializeField] List<FadeController> monumentFadeController;
    //List of GameObject of arck that needs to be activated
    [SerializeField] List<GameObject> ArckActivators;

    //Specific Holder for ArckOfTriumph due to it different design architecture
    [SerializeField] GameObject arckHolder;

    // This part of the code is written specifically for triumph arck, the code needs to be refractored 
    // due to time limitation we are writing something specific for triumph
    
    // Specific to ArckOfTriumph. List of gameObjects to be activated for the arck of triumph
    [SerializeField] List<GameObject> triumphActivators;
    //Specific to ArckOfTriumph. List of BoxCollider for the Arck of Triumph
    [SerializeField] List<BoxCollider> colliders;
    //Specific to ArckOfTriumph. List of inner box colliders (for the TriumphArch) that needs to be activated later 
    [SerializeField] List<BoxCollider> innerColliders;
    //List of gameObjects needed to be deactivated on being clicked Reset
    [SerializeField] List<GameObject> deactivateOnReset;

    //the position of the gameObject
    Vector3 position;
    //the initial position of the gameObject when the gameObject first loads
    Vector3 initialPosition;
    //the rotation of the gameObject
    Vector3 rotation;
    //the initial rotation of the gameObject when the gameObject first loads
    Vector3 initialRotation;
    //the initial scale of the gameObject when the gameObject first loads
    Vector3 initialScale;

    //when the dissolve effect/fade effect of the first monument that was loaded should not be deactivated/disappeared on loading additional gameObjects then click this or set this to true.
    [SerializeField] bool doNotDeactivateFirstDissolveEffect = false;
    //Specific for Beauty Monument. Click this or Set this to true if monument is Beauty
    [SerializeField] bool isBeauty = false;

    //Specific for Beauty Monument. reset the local position of the first child when resetting the monument.
    [Tooltip("Also reset first child local position when resetting the monument into map.")]
    [SerializeField] bool resetFirstChildPosition;
    
    //set to true when float sequence starts
    bool startFloatSequence = false;
    //set to true when float sequence ends
    bool startDeFloatSequence = false;

    //set to true when gaze control functionality is allowed
    bool gazeControlStatus = true;
    //set to true when monument is floating
    bool isFloating = false;

    private PhotonView pv;

    private void Awake() {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        initialPosition = transform.localPosition;
        position = initialPosition;

        initialScale = transform.localScale;

        rotation = new Vector3(0,1,0);
        initialRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);

        foreach(BoxCollider innerCollider in innerColliders)
        {
            innerCollider.enabled = false;
        }
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

    //functionality to Rotate the gameObject 
    void RotateObject()
    {
        transform.Rotate(rotation * Time.deltaTime * floatRotateSpeed);
    }

    // Starts the Floating Sequence when gazed at
    public void StartFloatSequence()
    {
        pv.RPC("RPC_StartFloatSequence", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_StartFloatSequence() {
        if (gazeControlStatus) {
            startFloatSequence = true;
            startDeFloatSequence = false;
            isFloating = true;
        }
    }

    //Starts the De-Floating Sequence when gaze removed
    public void StartDeFloatSequence()
    {
        pv.RPC("RPC_StartDeFloatSequence", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_StartDeFloatSequence() {
        if (gazeControlStatus) {
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

    // handles the floating functionalities
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
    
    //handles the defloating functionalities
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

    //handles the scaling functionality
    void Scale()
    {
        float scaleValue = Time.deltaTime * scaleFactor * floatScaleSpeed;
        transform.localScale += new Vector3(scaleValue,scaleValue,scaleValue);
    }

    // handles the descaling functionality
    void DeScale()
    {
        float scaleValue = Time.deltaTime * scaleFactor * floatScaleSpeed;
        if(transform.localScale.x >= initialScale.x)
            transform.localScale -= new Vector3(scaleValue,scaleValue,scaleValue);
    }

    //Sets gazeControlStatus to false so that the monument no longer floats and defloats on gaze
    public void DeactivateOnlyGazeControl()
    {
        pv.RPC("RPC_DeactivateOnlyGazeControl", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_DeactivateOnlyGazeControl() {
        gazeControlStatus = false;
    }

    //Ends the entire Gaze Control Sequence when the user handles the monument by hand
    public void DeactivateGazeControlSequence() //Deactivates the GazeControl
    {
        pv.RPC("RPC_DeactivateGazeControlSequence", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_DeactivateGazeControlSequence() {
        StartCoroutine(ActivateButtonGameObjects());
    }

    //activates button GameObjects
    IEnumerator ActivateButtonGameObjects()
    {
        yield return new WaitForSeconds(buttonObjectsActivationDelay);
        SetUpButtons();
    }

    //Sets up the buttons
    private void SetUpButtons() {
        if(buttons.transform.parent != null) {
            GameObject container = new GameObject();
            container.transform.parent = buttons.transform.parent;
            container.transform.position = buttons.transform.position;
            buttons.transform.parent = null;
            buttons.transform.localScale = Vector3.one;
            FollowPoint followPoint = buttons.AddComponent<FollowPoint>();
            followPoint.pointToFollow = container.transform;
            followPoint.speed = 30;
        }
        buttons.SetActive(true);
    }

    //Activates all additional Game Objects that are required when monument is scaled and handled by user. 
    //This process includes activating high poly models, deactivating holders, activating specific requirements like 
    //triumphActivators, etc.
    public void ActivateAllAdditionalObjects() {
        pv.RPC("RPC_ActivateAllAdditionalObjects", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_ActivateAllAdditionalObjects()
    {
        if(!doNotDeactivateFirstDissolveEffect)
        {
            if(dissolveEffect.Count != 0)
            {
                dissolveEffect[0].InitiateDisappearence();
            }
                
            if(monumentFadeController.Count != 0)
            {
                monumentFadeController[0].StartFadeOutSequence();
            }

            foreach(GameObject fadeout in ArckActivators)
            {
                fadeout.SetActive(false);
            }
        }
        foreach(BoxCollider collider in colliders)
        {
            collider.enabled = false;
        }
        foreach(BoxCollider innerCollider in innerColliders)
        {
            innerCollider.enabled = true;
        }
        StartCoroutine(ActivateHighPolyModels());
    }

    IEnumerator ActivateHighPolyModels()
    {
        yield return null;

        if(arckHolder != null)
        {
            arckHolder.SetActive(false);
        }

        foreach(GameObject gameObject in additionalActivators)
        {
            gameObject.SetActive(true);
        }

        if(dissolveEffect.Count != 0)
        {
            for(int i=1; i<dissolveEffect.Count; i++)
            {
                dissolveEffect[i].InitiateAppearence();  //fade in all other secondary items with dissolve shaders
            }
        }
       
        if(monumentFadeController.Count != 0)
        {
            for(int i=1; i<monumentFadeController.Count; i++)
            {
                monumentFadeController[i].StartFadeInSequence();
            }
        }
        
        foreach(GameObject fadein in triumphActivators)
        {
            fadein.SetActive(true);
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
    
    // Restarts the entire Gaze Control Sequence for when monument is back on map
    public void ActivateGazeControlSequence() //Activates the GazeControl
    {
        gazeControlStatus = true;

        if(dissolveEffect.Count != 0)
        {
            for(int i=1; i<dissolveEffect.Count; i++)
            {
                dissolveEffect[i].InitiateDisappearence(); //fade out all other secondary items with dissolve shaders
            }
        }
        
        if(monumentFadeController.Count != 0)
        {
            for(int i=1; i<monumentFadeController.Count; i++)
            {
                monumentFadeController[i].StartFadeOutSequence();
            }
        }
        
        foreach(GameObject gameObject in additionalActivators)
        {
            gameObject.SetActive(false);
        }
        buttons.SetActive(false);
    }

    //Handles resetting the monuments to its initial location on the map, deactivating additional gameObjects, reactivating holders, and other specific commands.
    public void InitiateResetPlaygroundMap() {
        pv.RPC("RPC_InitiateResetPlaygroundMap", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_InitiateResetPlaygroundMap()
    {   
        foreach(GameObject gameObject in holderDeactivators)
        {
            gameObject.SetActive(true);
        }

        foreach (var item in deactivateOnReset) {
            item.SetActive(false);
        }

        if(!doNotDeactivateFirstDissolveEffect && isBeauty)
        {
            if(dissolveEffect.Count != 0)
            {   
                dissolveEffect[0].InitiateAppearence();
            }
            
            if(monumentFadeController.Count != 0)
            {
                monumentFadeController[0].StartFadeInSequence();
            }

            foreach(GameObject fadein in ArckActivators)
            {
                fadein.SetActive(true);
            }
            StartCoroutine(GoAwayBeautyObjects());
            StartCoroutine(WaitToDisaapear());
        }

        foreach(BoxCollider collider in colliders)
        {
            collider.enabled = true;
        }

        foreach(BoxCollider innerCollider in innerColliders)
        {
            innerCollider.enabled = false;
        }
    
        for(int i=0; i<dissolveEffect.Count; i++)
        {
            dissolveEffect[i].InitiateDisappearence(); 
        }

        for(int i=0; i<monumentFadeController.Count; i++)
        {
            monumentFadeController[i].StartFadeOutSequence();
        }

        foreach(GameObject fadeout in triumphActivators)
        {
            fadeout.SetActive(false);
        }

        StartCoroutine(ResetPlaygroundMap());
        if(arckHolder != null)
        {
            arckHolder.SetActive(true);
        }
    }

    IEnumerator GoAwayBeautyObjects()
    {
        yield return new WaitForSeconds(1.5f);
        foreach(GameObject gameObject in additionalActivators)
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator WaitToDisaapear()
    {
        yield return new WaitForSeconds(3);

        if(dissolveEffect.Count != 0)
        {
            dissolveEffect[0].InitiateDisappearence();
        }
        
        if(monumentFadeController.Count != 0)
        {
            monumentFadeController[0].StartFadeOutSequence();
        }
        
        foreach(GameObject fadeout in ArckActivators)
        {
            fadeout.SetActive(false);
        }
    }

    IEnumerator ResetPlaygroundMap()
    {
        yield return new WaitForSeconds(delayForReset);
        transform.localPosition = initialPosition;

        //Reset also the first child local position
        if (resetFirstChildPosition) {
            transform.GetChild(0).localPosition = Vector3.zero;
        }

        if(dissolveEffect.Count != 0)
        {
            dissolveEffect[0].InitiateAppearence();
        }
        
        if(monumentFadeController.Count != 0)
        {
            monumentFadeController[0].StartFadeInSequence();
        }
       
        foreach(GameObject fadein in ArckActivators)
        {
            fadein.SetActive(true);
        }
        transform.localRotation = Quaternion.Euler(initialRotation); 
        transform.localScale = initialScale;
        ActivateGazeControlSequence();
    }

    //Initiates the appearance sequence for the monument
    public void InitiateAppearanceOfObject()
    {
        StartCoroutine(AppearObject());
    }

    IEnumerator AppearObject()
    {
        yield return new WaitForSeconds(3);
        transform.localPosition = initialPosition;

        if(dissolveEffect.Count != 0)
        {
            dissolveEffect[0].InitiateAppearence();
        }
        
        if(monumentFadeController.Count != 0)
        {
            monumentFadeController[0].StartFadeInSequence();
        }
        

        foreach(GameObject fadein in ArckActivators)
        {
            fadein.SetActive(true);
        }
        transform.localRotation = Quaternion.Euler(initialRotation); 
        transform.localScale = initialScale;
        ActivateGazeControlSequence();
    }

    //resets the value of fadecontroller and dissolve effect script to transparent states to begin with.
    public void ResetAppearanceValueDissolve()
    {
        if(dissolveEffect.Count != 0)
        {
            dissolveEffect[0].ResetAppearanceValue();
        }
            
        if(monumentFadeController.Count != 0)
        {
            monumentFadeController[0].ResetFadeValues();
        }
            
        foreach(GameObject fadeout in ArckActivators)
        {
            fadeout.SetActive(false);
        }
    }    
}
