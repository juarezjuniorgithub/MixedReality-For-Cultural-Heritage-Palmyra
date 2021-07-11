using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material[] dissolveMat;
    [SerializeField] float appearanceValue;
    [SerializeField] float appearanceSpeed=10f;
    [SerializeField] float appearanceLimit=100f;
    [SerializeField] string dissolveVal;
    [SerializeField] float minApperanceValue;
    float maxAppearanceValue=100f;
    bool initiateAppearanceSequence = false;
    bool initiateDisappearanceSequence = false;
    public Action onAppearEnded;
    public Action onDisappearStarting;


    void Start()
    {
        foreach (Material mat in dissolveMat)
        {
            mat.SetFloat(dissolveVal, appearanceValue/maxAppearanceValue);
        }
    }

    public void ResetAppearanceValue()
    {
        appearanceValue = 0;
        foreach (Material mat in dissolveMat)
        {
            mat.SetFloat(dissolveVal, appearanceValue/maxAppearanceValue);
        }
    }
    
    void Update()
    {
        if(initiateAppearanceSequence)
        {
            Appear();
        }
        else if(initiateDisappearanceSequence)
        {
            Disappear();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            InitiateAppearence();
        }     
        else if(Input.GetKeyDown(KeyCode.U))
        {
            InitiateDisappearence();
        }     
    }

    void Appear()
    {
        foreach(Material mat in dissolveMat)
        {
            if(appearanceValue<=maxAppearanceValue && appearanceValue<=appearanceLimit)
            {
                appearanceValue +=5 * Time.deltaTime * appearanceSpeed;
                mat.SetFloat(dissolveVal, appearanceValue/maxAppearanceValue);
            }
            else
            {
                if(appearanceLimit>100)
                {
                    foreach(Material mat2 in dissolveMat)
                    {
                        mat2.SetFloat(dissolveVal, 1.0f);
                    }
                }     
                initiateAppearanceSequence = false;
            }
        }
        if (!initiateAppearanceSequence) {
            onAppearEnded?.Invoke();
        }
    }

    void Disappear()
    {
        foreach (Material mat in dissolveMat)
        {
            if(appearanceValue>=minApperanceValue)
            {
                appearanceValue -=5 * Time.deltaTime * appearanceSpeed;
                mat.SetFloat(dissolveVal, appearanceValue/maxAppearanceValue);
            }
            else
            {
                initiateDisappearanceSequence = false;
            }
        }  
    }

    public void InitiateAppearence()
    {
        initiateAppearanceSequence = true;
        initiateDisappearanceSequence = false;
    }

    public void InitiateDisappearence()
    {
        onDisappearStarting?.Invoke();
        initiateDisappearanceSequence = true;
    }
}
