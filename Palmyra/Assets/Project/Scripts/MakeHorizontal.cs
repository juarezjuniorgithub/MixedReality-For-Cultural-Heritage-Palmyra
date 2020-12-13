using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHorizontal : MonoBehaviour
  
{
    public Transform[] elements;
    public Transform target;
    public void Horizontal()
    {
        transform.eulerAngles = new Vector3(0, target.rotation.eulerAngles.y, 0);
        
        for (int i = 0; i < elements.Length; i++)
        {

       
            elements[i].transform.eulerAngles = new Vector3(0, elements[i].transform.eulerAngles.y, elements[i].transform.eulerAngles.z);
        }
    }
}

