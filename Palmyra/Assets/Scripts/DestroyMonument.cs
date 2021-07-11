using MRTK.Tutorials.GettingStarted;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DestroyMonument : MonoBehaviour
{
    public Animator monumentanim;
    //public GameObject ornaments;
    public GameObject[] stoneglitch;
    public UnityEvent OnMonumentDestroyed;
    public GameObject[] stoneToSnap;
    public MeshRenderer[] stoneToSnapMAT;
    public Material mat;
    public GameObject[] stoneOfAnim;
    public GameObject[] stoneMainLocation;
    private GameObject destroyButton;
    private GameObject rebuildButton;
    GameObject[] gos;



    void Start()
    {
        destroyButton = GameObject.FindGameObjectWithTag("Destroy");
        rebuildButton = GameObject.FindGameObjectWithTag("Rebuild");
    }

    private void Awake()
    {
        //Destroy();
        //Rebuild();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("H Pressed");
            
        }
    }

    public void OnDestroyFinished()
    {
        Debug.Log("this is a destroy test");
        //rebuildButton.GetComponent<Interactable>().enabled = true;
        //rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = true;
    }

    public void OnRebuildFinished()
    {
        Debug.Log("this is a rebuilt test");
        //destroyButton.GetComponent<Interactable>().enabled = true;
        //destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = true;

    }

    public void Destroy()
    {

        monumentanim.SetTrigger("destroybaal");
        //ornaments.SetActive(false);
        //destroyButton.GetComponent<Interactable>().enabled = false;
        //destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(StoneToSnap());
        StartCoroutine(TrackDestroyAnim());
       

        Debug.Log("Botton Off");
    }

    public void Rebuild()
    {
        monumentanim.SetTrigger("rebuildbaal");
       
        //rebuildButton.GetComponent<Interactable>().enabled = false;
        //rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(Waitrebuild());
        StartCoroutine(WaittoActive());
        
        Debug.Log("Botton Off"); 
    }
    IEnumerator Waitrebuild()
    {
        

        yield return new WaitForSeconds(5.0f);
        //MonumetAppearOnDestruction();
        //ornaments.SetActive(true);
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
        yield return new WaitForSeconds(5.0f);
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
        yield return new WaitForSeconds(5.0f);
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

    public void MonumetRemoveOnDestruction()
    {
        
        gos =GameObject.FindGameObjectsWithTag("HideBaalP");
        foreach (GameObject item in gos)
        {
            item.SetActive(false);
            // TODOÜ Creante new dissolve mat for higebaalp - add to dissolve script - call initiate disapperance

        }
    }

    public void MonumetAppearOnDestruction()
    {

        foreach (GameObject item in gos)
        {
            item.SetActive(true);
            // TODO call initiate apperance on script
         }
    }

}
