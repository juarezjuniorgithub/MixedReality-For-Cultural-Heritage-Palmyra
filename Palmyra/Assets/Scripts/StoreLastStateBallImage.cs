using Photon.Pun;
using UnityEngine;

public class StoreLastStateBallImage : MonoBehaviourPun {

    [SerializeField] GameObject baalBefore;
    [SerializeField] GameObject baalAfter;
    bool b1=true;
    bool b2=false;
    public GameObject currentState;

    void Start()
    {
        currentState = baalBefore;
    }

    public void SetTempleImages() {
        photonView.RPC("RPC_SetTempleImages", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_SetTempleImages() {
        if (currentState == null) return;
        if (currentState == baalBefore) {
            baalBefore.SetActive(true);
            baalAfter.SetActive(false);
        } else {
            baalBefore.SetActive(false);
            baalAfter.SetActive(true);
        }
    }

    public void ChangeState() {
        photonView.RPC("RPC_ChangeState", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_ChangeState()
    {
        if(b1)
        {
            b1=false;
            currentState = baalAfter;
            b2=true;
        }
        else if(b2)
        {
            b2=false;
            currentState = baalBefore;
            b1=true;
        }
    }
    
    public GameObject GetCurrentState()
    {
        return currentState;
    }
}
