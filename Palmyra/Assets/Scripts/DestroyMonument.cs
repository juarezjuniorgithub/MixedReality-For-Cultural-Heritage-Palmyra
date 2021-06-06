using MRTK.Tutorials.GettingStarted;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DestroyMonument : MonoBehaviour
{
    public Animator monumentanim;
    public GameObject ornaments;
    public GameObject[] stoneglitch;
    public UnityEvent OnMonumentDestroyed;
    public GameObject[] stoneToSnap;
    public MeshRenderer[] stoneToSnapMAT;
    public Material mat;
    public GameObject[] stoneOfAnim;
    public GameObject[] stoneMainLocation;
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
        rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = true;
    }

    public void OnRebuildFinished()
    {
        Debug.Log("this is a rebuilt test");
        destroyButton.GetComponent<Interactable>().enabled = true;
        destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = true;

    }

    public void Destroy()
    {

        monumentanim.SetTrigger("destroy");
        ornaments.SetActive(false);
        destroyButton.GetComponent<Interactable>().enabled = false;
        destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(StoneToSnap());
        StartCoroutine(TrackDestroyAnim());
       

        Debug.Log("Botton Off");
    }

    public void Rebuild()
    {
        rebuildButton.GetComponent<Interactable>().enabled = false;
        rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(Waitrebuild());
        StartCoroutine(WaittoActive());
        Debug.Log("Botton Off"); 
    }
    IEnumerator Waitrebuild()
    {
        monumentanim.SetTrigger("rebuild");

        yield return new WaitForSeconds(6.210f);
        ornaments.SetActive(true);
        foreach (GameObject stones in stoneOfAnim )
        {
            stones.SetActive(true);
        }

        foreach (GameObject stones in stoneToSnap)
        {
            stones.SetActive(false);
            stones.GetComponent<MeshRenderer>();
            
        }
        
        foreach (GameObject stones in stoneMainLocation)
        {
            stones.SetActive(false);
        }
    }
    IEnumerator TrackDestroyAnim()
    {
        yield return new WaitForSeconds(6.210f);
    }

    IEnumerator WaittoActive()
    {
        yield return new WaitForSeconds(5.64f);
        foreach (GameObject target in stoneglitch)
        {
            target.SetActive(true);
        }
    }

    IEnumerator StoneToSnap()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        yield return new WaitForSeconds(6.1f);
        foreach (GameObject stones in stoneOfAnim)
        {
            stones.SetActive(false);
        }
        
        foreach (GameObject stones in stoneToSnap)
        {
            stones.SetActive(true);
            stones.GetComponent<PartAssemblyController>().ResetPlacement();
        

     
            
        }

        foreach (GameObject stones in stoneToSnap)
        {
            foreach (MeshRenderer item in stoneToSnapMAT)
            {
                item.GetComponent<MeshRenderer>();
                item.material = mat;
            }

        }

        foreach (GameObject stones in stoneMainLocation)
        {
            stones.SetActive(true);
        }
    }
}
