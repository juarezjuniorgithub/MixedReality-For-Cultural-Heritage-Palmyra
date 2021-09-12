using Photon.Pun;
using UnityEngine;

public class BeautyHolder : MonoBehaviourPun
{
    public void DisableBoxColliderOfBeauty() {
        photonView.RPC("RPC_DisableBoxColliderOfBeauty", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_DisableBoxColliderOfBeauty()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnableBoxColliderOfBeauty() {
        photonView.RPC("RPC_EnableBoxColliderOfBeauty", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_EnableBoxColliderOfBeauty()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
