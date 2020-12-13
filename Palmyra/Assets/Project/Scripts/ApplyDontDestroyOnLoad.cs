using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDontDestroyOnLoad : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



}
