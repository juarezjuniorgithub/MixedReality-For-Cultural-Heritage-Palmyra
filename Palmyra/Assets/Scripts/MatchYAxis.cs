using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchYAxis : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, target.rotation.eulerAngles.y, 0);
        
        if (target.position.y > 0)
        {

            transform.localPosition = new Vector3(0, 0, 0);

        }
        else
        {
            transform.localPosition = new Vector3(0,transform.localPosition.y, 0);
        }
    }
}
