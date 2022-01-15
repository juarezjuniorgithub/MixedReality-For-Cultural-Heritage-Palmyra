using Photon.Pun;
using UnityEngine;

public class SwitchBaalImage : MonoBehaviourPun {

    [SerializeField] Texture2D baalDestroy;
    [SerializeField] Texture2D baalRebuild;
    public Monument monument;

    public void SetImageToRebulid()
    {
        photonView.RPC("RPC_SetImageToRebuild", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void RPC_SetImageToRebuild()
    {
        if(monument.localTextPanel != null)
        {
            monument.localTextPanel.image.texture = baalRebuild;
        }
    }
    
    public void SetImageToDestroy()
    {
        photonView.RPC("RPC_SetImageToDestroy", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void RPC_SetImageToDestroy()
    {
        if (monument.localTextPanel != null)
        {
            monument.localTextPanel.image.texture = baalDestroy;
        }
    }

    public void ChangeState() {
        //photonView.RPC("RPC_ChangeState", RpcTarget.AllBuffered);
    }
}
