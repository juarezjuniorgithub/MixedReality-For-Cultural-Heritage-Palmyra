using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectYRotation : MonoBehaviour
{
    public Vector3 currentRotation;
    public GameObject container;


    void Start()
    {
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y / 360f, currentRotation.z / 360);
        container.transform.eulerAngles = currentRotation;
    }

    /*{
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y / 360f, currentRotation.z / 360);
        container.transform.eulerAngles = currentRotation;
    }*/

}
