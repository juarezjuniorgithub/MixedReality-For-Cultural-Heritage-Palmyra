using Photon.Pun;
using UnityEngine;

public class StoreLastStateBallImage : MonoBehaviourPun {

    [SerializeField] Texture2D baalDestroy;
    [SerializeField] Texture2D baalRebuild;
    public Texture2D currentState;
    public Monument monument;


    void Start()
    {
        currentState = baalRebuild;
    }

    public void SetImageToRebulid()
    {
        photonView.RPC("RPC_SetImageToRebuild", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void RPC_SetImageToRebuild()
    {
        monument.textPanelImage = baalRebuild;
        
    }
    
    public void SetImageToDestroy()
    {
        photonView.RPC("RPC_SetImageToDestroy", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void RPC_SetImageToDestroy()
    {
        monument.textPanelImage = baalDestroy;

    }

    /*[PunRPC]
    public void RPC_SetTempleImages() {
        if (currentState == null) return;
        if (currentState == baalBefore) {
            monument.textPanelImage = baalBefore;
            //baalBefore.SetActive(true);
            //baalAfter.SetActive(false);
        } else {
            //baalBefore.SetActive(false);
            //baalAfter.SetActive(true);
        }
    }*/

    public void ChangeState() {
        //photonView.RPC("RPC_ChangeState", RpcTarget.AllBuffered);
    }

    //[PunRPC]
   /* public void RPC_ChangeState()
    {
        if(baalBefore.activeInHierarchy)
        {
            currentState = baalAfter;
        }
        else
        {
            currentState = baalBefore;
        }
    }*/
    
    /*public GameObject GetCurrentState()
    {
        return currentState;
    }*/
}
