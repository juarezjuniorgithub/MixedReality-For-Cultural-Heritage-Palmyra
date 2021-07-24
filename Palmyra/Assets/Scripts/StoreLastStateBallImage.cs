using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLastStateBallImage : MonoBehaviour
{
    [SerializeField] GameObject baalBefore;
    [SerializeField] GameObject baalAfter;
    bool b1=true;
    bool b2=false;
    public GameObject currentState;

    void Start()
    {
        currentState = baalBefore;
    }

    public void ChangeState()
    {
        if(b1)
        {
            b1=false;
            currentState = baalAfter;
            b2=true;
        }
        else if(b2)
        {
            b2=false;
            currentState = baalBefore;
            b1=true;
        }
    }
    
    public GameObject GetCurrentState()
    {
        return currentState;
    }
}
