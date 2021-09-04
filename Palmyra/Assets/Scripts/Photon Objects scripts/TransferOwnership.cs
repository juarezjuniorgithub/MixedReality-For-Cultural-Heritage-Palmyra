using Photon.Pun;

public class TransferOwnership : MonoBehaviourPun
{
    public void TransferOwnershipWhenGrab() {
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
