using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAnimation : MonoBehaviour
{
    public GameObject syriaSurface;
    public Vector3 orignalPosition;
    public Quaternion originalRotation;
    public Vector3 originalScale;
    public GameObject tooltip;
    public MeshRenderer MeshrenderSyria;
    

    // Start is called before the first frame update
    void Awake()
    {
        orignalPosition = transform.position;
        originalScale = transform.lossyScale;
        originalRotation = transform.rotation;
        gameObject.SetActive(false);
        MeshrenderSyria.enabled = false;
        

        
    }
    /*public void GrowFunc()
    {
        int x = 0;
        while (x<1)
        {
            Grow();
            x++;
            Debug.Log("LOL");
        }
        
    }*/

    public void Grow()
    {
      
       StartCoroutine(CoGrow());


    }

    IEnumerator CoGrow()
    {

        
        yield return new WaitForSeconds(2);
        MeshrenderSyria.enabled = true;
        for (float i = 0; i < 100; i++)
        {
           
            transform.position= Vector3.Lerp(syriaSurface.transform.position, orignalPosition, i / 99f);
            transform.localScale= Vector3.Lerp(syriaSurface.transform.lossyScale, originalScale, i / 99f);
            transform.rotation= Quaternion.Lerp(syriaSurface.transform.rotation, originalRotation, i / 99f);
            yield return new WaitForSeconds(1/99f);
        }
        tooltip.SetActive(true);
    }
}
