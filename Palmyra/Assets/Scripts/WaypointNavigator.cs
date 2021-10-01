using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] Transform nextPosition;
    [SerializeField] float deltaAngle;
    [SerializeField] private Transform cameraForward;
    private int index = 0;

    void Start()
    {
        nextPosition = waypoints[index];
    }

    void Update()
    {
        if(nextPosition != null) {
            transform.forward = Vector3.ProjectOnPlane(Vector3.up, nextPosition.position - transform.position);
            deltaAngle = Quaternion.Angle(cameraForward.transform.rotation, transform.rotation);
            if(Vector3.Distance(nextPosition.position, transform.position) < 0.5f) {
                if(index < waypoints.Count) {
                    index++;
                    nextPosition = waypoints[index];
                    if(index == waypoints.Count) {
                        Debug.Log("Finished");
                    }
                }
            }
        }
    }
}
