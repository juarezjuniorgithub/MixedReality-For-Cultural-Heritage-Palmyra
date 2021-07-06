using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautyHolder : MonoBehaviour
{

    public void DisableBoxColliderOfBeauty()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnableBoxColliderOfBeauty()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
