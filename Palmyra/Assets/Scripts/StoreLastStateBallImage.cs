using Photon.Pun;
using UnityEngine;

public class StoreLastStateBallImage : MonoBehaviourPun {

    [SerializeField] GameObject baalBefore;
    [SerializeField] GameObject baalAfter;
    public GameObject currentState;

    void Start()
    {
        currentState = baalBefore;
    }

    public void SetTempleImages() {
        if(photonView != null) {
            photonView.RPC("RPC_SetTempleImages", RpcTarget.AllBuffered);
        }
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
        if(baalBefore.activeInHierarchy)
        {
            currentState = baalAfter;
        }
        else
        {
            currentState = baalBefore;
        }
    }
    
    public GameObject GetCurrentState()
    {
        return currentState;
    }
}
