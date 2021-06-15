using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContainerButtonPositioner : MonoBehaviour
{
    public GameObject buttons;
    public float distanceFromQRCodeinCM=35f;
    public float heightFromTableinCM = 15f;

    void  Start()
    {
        UpdateDistanceOfButton();
    }

    void UpdateDistanceOfButton()
    {
        buttons.transform.localPosition = new Vector3(0.053f,heightFromTableinCM/100,-distanceFromQRCodeinCM/100);
    }
}
