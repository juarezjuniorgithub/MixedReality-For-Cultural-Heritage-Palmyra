using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUser : MonoBehaviour
{
    enum Target { User, Waypoint}
    [SerializeField] Target target = Target.Waypoint;
    [SerializeField] WaypointNavigator waypointNavigator;
    private Transform destination;

    // Update is called once per frame
    void Update()
    {
        if (target == Target.User) {
            Vector3 target = destination.transform.position + destination.transform.forward * 1 + destination.transform.up * Mathf.Sin(Time.time) * 0.1f;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
            transform.LookAt(destination.transform.position);
        } else if (target == Target.Waypoint && waypointNavigator.nextPosition != null){
            destination = waypointNavigator.nextPosition;
            Vector3 target = destination.transform.position + destination.transform.up * 1.5f + destination.transform.up * Mathf.Sin(Time.time) * 0.1f;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
            transform.LookAt(destination.transform.position);
        }
    }
}
