using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
   
    [SerializeField] float scaleValue=0f;
    [SerializeField] float maxScaleValue;
    [SerializeField] float scaleSpeed=1f;
    float initialScaleX;
    
    bool initiateScaleSequence = false;
    bool initiateDescaleSequence = false;

    void Start()
    {
        initialScaleX = transform.localScale.x;
    }
    
    void Update()
    {
        
        if(initiateScaleSequence)
        {
            Scale();
        }

        if(initiateDescaleSequence)
        {
            Descale();
        }
    }

    void Scale()
    {
            if((transform.localScale.x-initialScaleX)<=maxScaleValue)
            {
                scaleValue = Time.deltaTime * scaleSpeed;
                transform.localScale += new Vector3(scaleValue, scaleValue, scaleValue);
            }
            else
            {
                initiateScaleSequence = false;
            }
    }

    void Descale()
    {
            if(transform.localScale.x>=initialScaleX)
            {
                scaleValue = Time.deltaTime * scaleSpeed;
                transform.localScale -= new Vector3(scaleValue, scaleValue, scaleValue);
            }
            else
            {
                initiateDescaleSequence = false;
            }
    }

    public void InitiateScaling()
    {
        initiateScaleSequence = true;
    }

    public void InitiateDescaling()
    {
        initiateDescaleSequence = true;
    }
}