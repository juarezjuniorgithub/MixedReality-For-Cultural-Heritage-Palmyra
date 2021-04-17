using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlacementController : MonoBehaviour
{
    public DestroyMonument destroyMonument;
    // Start is called before the first frame update
    void Start()
    {
        //Duplicate the original stone and create a snappoint
        destroyMonument.OnMonumentDestroyed.AddListener(CreateStoneToPlace);
        //Duplicate game object
        GameObject duplicate = Instantiate(gameObject);
        //Copy position, rotation, scale of original to duplicate.



        //Add box collider to the duplicated object

        //Change material to Xray

    }
    public void CreateStoneToPlace()
    {


    }

 
}
