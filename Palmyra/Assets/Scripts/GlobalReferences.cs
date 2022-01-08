using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences instance;

    public Camera localUser;

    void Awake()
    {
        instance = this;
    }
}
