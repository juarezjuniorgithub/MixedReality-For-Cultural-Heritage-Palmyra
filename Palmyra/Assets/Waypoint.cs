using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public enum WaypointType { Regular, Dog, Stairs, Finish}
    public WaypointType waypointType = WaypointType.Regular;
    [HideInInspector] public bool triggered;
}
