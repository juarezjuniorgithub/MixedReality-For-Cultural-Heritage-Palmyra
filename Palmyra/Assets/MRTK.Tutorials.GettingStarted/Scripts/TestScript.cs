using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
       // parent = FindObjectOfType<Container>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSize()
    {
        Debug.Log("SIZE= " + gameObject.transform.localScale);
        Debug.Log("MaxDistance= " + gameObject.transform.localScale / 10);
    }
}
