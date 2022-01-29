using System;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material[] dissolveMat;
    [SerializeField] float appearanceValue;
    [SerializeField] float appearanceSpeed=10f;
    [SerializeField] float appearanceLimit=100f;
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
            SetMatAlpha(mat, 0);
        }
    }

    public void ResetAppearanceValue()
    {
        appearanceValue = 0;
        foreach (Material mat in dissolveMat)
        {
            SetMatAlpha(mat, appearanceValue);
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
                appearanceValue += 5 * Time.deltaTime * appearanceSpeed;
                SetMatAlpha(mat, appearanceValue);
            }
            else
            {
                if(appearanceLimit >= maxAppearanceValue)
                {
                    foreach(Material mat2 in dissolveMat)
                    {
                        SetMatAlpha(mat2, maxAppearanceValue);
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
            if(appearanceValue >= minApperanceValue)
            {
                appearanceValue -= 5 * Time.deltaTime * appearanceSpeed;
                SetMatAlpha(mat, appearanceValue);
            }
            else
            {
                initiateDisappearanceSequence = false;
            }
        }  
    }

    private void SetMatAlpha(Material mat, float value)
    {
        Color white = Color.white;
        white.a = value / 100f;
        mat.SetColor("_Color", white);
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
