using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

namespace MRFCH {
    public class ShowSliderValuePalmyra : MonoBehaviour {
        [SerializeField]
        private TextMeshPro textMesh = null;

        public void OnSliderUpdated(SliderEventData eventData) {
            if (textMesh == null) {
                textMesh = GetComponent<TextMeshPro>();
            }

            string year = "Year: " + (1 + (int)(eventData.NewValue * (2021 - 1))).ToString() + " AD";

            if (textMesh != null) {
                textMesh.text = year;
            }
        }
    }
}
