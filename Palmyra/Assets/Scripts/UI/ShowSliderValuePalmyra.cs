using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using Photon.Pun;

namespace MRFCH {
    public class ShowSliderValuePalmyra : MonoBehaviourPun {
        [SerializeField]
        private TextMeshPro textMesh = null;

        public void OnSliderUpdated(SliderEventData eventData) {
            photonView.RPC("RPC_UpdateYear", RpcTarget.All, eventData.NewValue);
        }

        [PunRPC]
        public void RPC_UpdateYear(float f) {
            if (textMesh == null) {
                textMesh = GetComponent<TextMeshPro>();
            }

            string year = "Year: " + (1 + (int)(f * (2021 - 1))).ToString() + " AD";

            if (textMesh != null) {
                textMesh.text = year;
            }
        }
    }
}
