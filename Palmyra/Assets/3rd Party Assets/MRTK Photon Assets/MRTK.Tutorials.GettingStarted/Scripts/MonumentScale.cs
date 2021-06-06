using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentScale : MonoBehaviour
{
    public static Vector3 currentScale;

    // Update is called once per frame
    void Update()
    {
        currentScale = gameObject.transform.localScale;
    }
}
