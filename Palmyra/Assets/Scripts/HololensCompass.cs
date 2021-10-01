using UnityEngine;

public class HololensCompass : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI compassText;

    // Update is called once per frame
    void Update()
    {
        compassText.text = Input.compass.magneticHeading.ToString(); 
    }
}
