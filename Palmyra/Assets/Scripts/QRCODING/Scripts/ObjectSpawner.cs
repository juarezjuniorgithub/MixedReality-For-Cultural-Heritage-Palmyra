using MRTKExtensions.QRCodes;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private QRTrackerController trackerController;

    private void Start()
    {
#if !UNITY_EDITOR
        trackerController.PositionSet += PoseFound;
#endif
#if UNITY_EDITOR
        PoseFound(transform.position);
#endif
    }

    private void PoseFound(object sender, Pose pose)
    {
        Quaternion newRotation;
        var childObj = transform.GetChild(0);

       // if(pose.rotation.eulerAngles.z > 2)
       // {
            newRotation = Quaternion.Euler( 0, 0, 0);
       // }
       // else
      //  {
       //     newRotation = pose.rotation;
       // }

        childObj.SetPositionAndRotation(pose.position, newRotation); //setting position and rotation
        
        Debug.Log("----------------ROTATION of POSE---------------------------------------");
        Debug.Log("X: "+pose.rotation.eulerAngles.x+", Y: "+pose.rotation.eulerAngles.y+", Z: "+pose.rotation.eulerAngles.z);
        Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

        Debug.Log("----------------NEW ROTATION---------------------------------------");
        Debug.Log("X: "+newRotation.eulerAngles.x+", Y: "+newRotation.eulerAngles.y+", Z: "+newRotation.eulerAngles.z);
        Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        
        childObj.gameObject.SetActive(true);
    }

    private void PoseFound(Vector3 pos) {
        Quaternion newRotation;
        var childObj = transform.GetChild(0);

        // if(pose.rotation.eulerAngles.z > 2)
        // {
        newRotation = Quaternion.Euler(0, 0, 0);
        // }
        // else
        //  {
        //     newRotation = pose.rotation;
        // }

        childObj.SetPositionAndRotation(pos, newRotation); //setting position and rotation

        childObj.gameObject.SetActive(true);
    }
}