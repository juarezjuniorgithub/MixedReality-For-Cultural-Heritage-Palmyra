using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(ObjectManipulator))]
public class TransferOwnership : MonoBehaviourPun
{
    ObjectManipulator objectManipulator;

    private void Start() {
        objectManipulator.OnManipulationStarted.AddListener(TransferOwnershipWhenGrab);
    }

    private void OnDestroy() {
        objectManipulator.OnManipulationStarted.RemoveListener(TransferOwnershipWhenGrab);
    }

    public void TransferOwnershipWhenGrab(ManipulationEventData arg0) {
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
