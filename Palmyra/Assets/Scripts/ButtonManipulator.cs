using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManipulator : MonoBehaviour
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] float waitTime = 0.5f;

    [SerializeField] StoreLastStateBallImage storeLastStateBallImage;

    void OnEnable()
    {
        storeLastStateBallImage.GetCurrentState().SetActive(true);
    }
    
    public void ActivateButton1()
    {
        button2.SetActive(false);
        StartCoroutine(Activate(button1));
    }

    public void ActivateButton2()
    {
        button1.SetActive(false);
        StartCoroutine(Activate(button2));
    }

    IEnumerator Activate(GameObject button)
    {
        yield return new WaitForSeconds(waitTime);
        button.SetActive(true);
    }

    
}
