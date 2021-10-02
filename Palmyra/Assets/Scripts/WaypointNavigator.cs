using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] List<Transform> waypoints;
    public Transform nextPosition;
    [SerializeField] AudioSource soundEmmitter;
    private int index = 0;

    void Start()
    {
        nextPosition = waypoints[index];
        soundEmmitter.transform.position = nextPosition.transform.position;
    }

    void Update()
    {
        if(nextPosition != null) {
            if(Vector3.Distance(nextPosition.position, camera.transform.position) < 0.5f) {
                if(index < waypoints.Count) {
                    index++;
                    nextPosition = waypoints[index];
                    soundEmmitter.transform.position = nextPosition.transform.position;
                    if (index == waypoints.Count) {
                        Debug.Log("Finished");
                        soundEmmitter.Stop();
                    }
                }
            }
        }
    }
}
