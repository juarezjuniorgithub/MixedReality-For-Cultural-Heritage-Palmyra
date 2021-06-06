using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
public class ControScale : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnSliderUpdated(SliderEventData eventData)
    {
        transform.localScale = eventData.NewValue * Vector3.one;
    }
}
