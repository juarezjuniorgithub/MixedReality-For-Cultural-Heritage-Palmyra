using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PalmyraImageNetworkHandler : MonoBehaviour
{
    //public GameObject palmyraImage;
    public GameObject sceneObjects;
    public Transform transformSceneObjects;
    public GameObject buildArck;
    //public BoxCollider buildButton; 
    public GameObject destroyArck;
    //public BoxCollider destroyButton;
    public GameObject Handmenu;
    public GameObject arckOfTriumph;
    //public GameObject airtapinstruction;
    public GameObject Earth;
    public GameObject Syria;
    public Transform sceneLight;
    public GameObject Audio1;
    public GameObject Audio2;
    public GameObject containerArckminiZaid;
    PhotonView photonView;
    //public MatchYAxis matchyAxis;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }
    [PunRPC]
    public void Activatecomponents()
    {
        //palmyraImage.SetActive(false);
        sceneObjects.SetActive(true);
        transformSceneObjects.transform.DetachChildren();
        buildArck.SetActive(true);
        //buildButton.enabled = true;
        destroyArck.SetActive(true);
        //destroyButton.enabled = true;
        Handmenu.SetActive(true);
        arckOfTriumph.SetActive(true);
        //airtapinstruction.SetActive(false);
        sceneLight.transform.DetachChildren();
        //matchyAxis.enabled
        Audio2.SetActive(true);
        containerArckminiZaid.SetActive(true);
        Earth.SetActive(false);
        Audio1.SetActive(false);
        Syria.SetActive(false);


    }

    public void ActivateComponnentsOnAllDevices()
    {
        photonView.RPC("Activatecomponents", RpcTarget.All);
    }

}
