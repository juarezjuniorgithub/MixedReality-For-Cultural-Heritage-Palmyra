using MRTKExtensions.QRCodes;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private QRTrackerController trackerController;
    [SerializeField] GameObject instructions;

    private void Start()
    {
        trackerController.PositionSet += PoseFound;
    }

    private void PoseFound(object sender, Pose pose)
    {
        var childObj = transform.GetChild(0);
        childObj.SetPositionAndRotation(pose.position, pose.rotation);
        childObj.gameObject.SetActive(true);
        instructions.SetActive(false); //deactivate instructions
    }
}