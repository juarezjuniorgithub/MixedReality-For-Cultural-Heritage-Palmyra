using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManipulator : MonoBehaviourPun
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] float waitTime = 0.5f;

    [SerializeField] SwitchBaalImage storeLastStateBallImage;



    public void ActivateButton1() {
        photonView.RPC("RPC_ActivateButton1", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_ActivateButton1()
    {
        button2.SetActive(false);
        StartCoroutine(Activate(button1));
    }
    public void ActivateButton2() {
        photonView.RPC("RPC_ActivateButton2", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_ActivateButton2()
    {
        button1.SetActive(false);
        StartCoroutine(Activate(button2));
    }

    IEnumerator Activate(GameObject button)
    {
        yield return new WaitForSeconds(waitTime);
        button.SetActive(true);
    }
}
