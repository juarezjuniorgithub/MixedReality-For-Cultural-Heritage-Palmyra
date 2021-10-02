using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] Camera cam;
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
            Vector3 waypointProjection = new Vector3(nextPosition.position.x, 0, nextPosition.position.z);
            Vector3 cameraProjection = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);

            ; if (Vector3.Distance(waypointProjection, cameraProjection) < 0.5f) {
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
