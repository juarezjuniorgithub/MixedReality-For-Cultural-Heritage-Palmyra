using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] Transform nextPosition;

    void Start()
    {
        nextPosition = waypoints[0];
    }

    void Update()
    {
        
    }
}
