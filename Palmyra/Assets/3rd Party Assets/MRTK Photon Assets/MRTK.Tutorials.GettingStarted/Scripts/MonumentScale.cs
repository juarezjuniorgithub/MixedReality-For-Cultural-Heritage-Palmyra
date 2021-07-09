using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentScale : MonoBehaviour
{
    public static Vector3 currentScale;
    [SerializeField] Vector3 additionValue = new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        currentScale = gameObject.transform.localScale + additionValue;
    }
}
