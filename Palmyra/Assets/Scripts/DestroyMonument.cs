using MRTK.Tutorials.GettingStarted;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyMonument : MonoBehaviourPun
{
    [SerializeField] List<FadeInFadeOut> fadeInFadeOuts;
    public Animator monumentanim;
    //public GameObject ornaments;
    public GameObject[] stoneglitch;
    public GameObject[] stoneToSnap;
    public MeshRenderer[] stoneToSnapMAT;
    public Material mat;
    public GameObject[] stoneOfAnim;
    public GameObject[] stoneMainLocation;

    GameObject[] gos;

    public void Destroy() {
        photonView.RPC("RPC_Destroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_Destroy()
    {
        monumentanim.SetTrigger("destroybaal");
        StartCoroutine(StoneToSnap());
        StartCoroutine(TrackDestroyAnim());
       
        Debug.Log("Botton Off");
    }

    public void Rebuild() {
        photonView.RPC("RPC_Rebuild", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_Rebuild()
    {
        monumentanim.SetTrigger("rebuildbaal");
       
        StartCoroutine(Waitrebuild());
        StartCoroutine(WaittoActive());
        
        Debug.Log("Botton Off"); 
    }

    IEnumerator Waitrebuild()
    {
        yield return new WaitForSeconds(5.0f);

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

        foreach(FadeInFadeOut fadeInFadeOut in fadeInFadeOuts)
        {
            fadeInFadeOut.StartFadeInSequence();
        }
        
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
