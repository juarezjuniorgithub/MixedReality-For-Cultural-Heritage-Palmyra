using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMonumentBeel : MonoBehaviour
{
    public Animator monumentanim;
    public GameObject ornaments;
    public GameObject [] fractured;
    

    public void Destroy()
    {
        
        ornaments.SetActive(false);
        foreach (var item in fractured)
        {
            item.SetActive(true);
        }
        monumentanim.SetTrigger("destroy");
        //StartCoroutine(WaitedGlitch());
    }

    public void Rebuild()
    {
        StartCoroutine(Waitrebuild());
        //StartCoroutine(WaittoActive());

    }
    IEnumerator Waitrebuild()
    {
        //yield return new WaitForSeconds(6.208f);
        monumentanim.SetTrigger("rebuild");

        yield return new WaitForSeconds(5.0f);
        ornaments.SetActive(true);
        foreach (var item in fractured)
        {
            item.SetActive(false);
        }
        //yield return new WaitForSeconds(6.13f);
    }
    IEnumerator WaitedGlitch()
    {
        yield return new WaitForSeconds(1.3f);
        foreach(GameObject target in fractured) 
        {
            target.SetActive(false);
        }

    }
    IEnumerator WaittoActive()
    {
        yield return new WaitForSeconds(5.64f);
        foreach (GameObject target in fractured)
        {
            target.SetActive(true);

        }



    }


}
