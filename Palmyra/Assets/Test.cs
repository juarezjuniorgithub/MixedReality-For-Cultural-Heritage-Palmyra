/*using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private GameObject destroyButton;
    private GameObject rebuildButton;

    void Start()
    {
        destroyButton = GameObject.FindGameObjectWithTag("Destroy");
        rebuildButton = GameObject.FindGameObjectWithTag("Rebuild");
    }

   

    public void OnDestroyFinished()
    {
        Debug.Log("this is a destroy test");
        rebuildButton.GetComponent<Interactable>().enabled = true;
    }

    public void OnRebuildFinished()
    {
        Debug.Log("this is a rebuilt test");
        destroyButton.GetComponent<Interactable>().enabled = true;

    }
}*/
