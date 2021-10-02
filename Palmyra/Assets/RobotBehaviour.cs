using Microsoft.MixedReality.Toolkit.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    public enum Target { User, Waypoint}
    [SerializeField] Target target = Target.Waypoint;
    [SerializeField] WaypointNavigator waypointNavigator;
    [SerializeField] Camera cam;
    private Transform destination;
    [SerializeField] AudioSource soundEmitter;
    [SerializeField] TextToSpeech textToSpeech;
    public enum State { ScanQR, Directing, Dog, Stairs, Finish}
    public State appState = State.ScanQR;

    private float timeCounter;

    private void Start() {
        ChangeAppState(State.ScanQR);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == Target.User) {
            soundEmitter.Stop();
            Vector3 target = cam.transform.position + cam.transform.forward * 1 + cam.transform.up * Mathf.Sin(Time.time) * 0.1f;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
            transform.LookAt(cam.transform.position);
        } else if (target == Target.Waypoint && waypointNavigator.nextPosition != null){
            soundEmitter.Play();
            destination = waypointNavigator.nextPosition;
            Vector3 target = destination.transform.position + destination.transform.up * 1.5f + destination.transform.up * Mathf.Sin(Time.time) * 0.1f;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
        }
        transform.LookAt(cam.transform.position);

        if(appState == State.ScanQR) {
            timeCounter += Time.deltaTime;
            if(Time.time - timeCounter > 5) {
                textToSpeech.StartSpeaking("Look for a QR code in the floor");
            }
        }       
    }

    public void SwitchTargetType(Target targetType) {
        target = targetType;
    }

    public void ChangeAppState(State newState) {
        switch (newState) {
            case State.ScanQR:
                SwitchTargetType(Target.User);
                break;
            case State.Directing:
                SwitchTargetType(Target.Waypoint);
                break;
            case State.Dog:
                break;
            case State.Stairs:
                break;
            case State.Finish:
                SwitchTargetType(Target.User);
                break;
            default:
                break;
        }
    }
}
