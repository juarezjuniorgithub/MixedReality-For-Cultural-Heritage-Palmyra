using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] RobotBehaviour robotBehaviour;
    [HideInInspector] public Transform nextPosition;
    private int index = 0;

    void Start()
    {
        nextPosition = waypoints[index];
    }

    void Update()
    {
        if(nextPosition != null) {
            if(Vector3.Distance(nextPosition.position, camera.transform.position) < 0.5f) {
                if(index < waypoints.Count) {
                    index++;
                    nextPosition = waypoints[index];
                    if (index == waypoints.Count) {
                        WaypointsCompleted();
                    }
                }
            }
        }
    }

    private void WaypointsCompleted() {
        Debug.Log("Waypoints completed");
        robotBehaviour.ChangeAppState(RobotBehaviour.State.Finish);
    }
}
