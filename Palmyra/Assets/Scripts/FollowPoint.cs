using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform pointToFollow;
    public float speed = 1;
    [SerializeField] bool copyRotation = false;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToFollow.position, Time.deltaTime * speed);
        if (copyRotation) {
            transform.rotation = pointToFollow.rotation;
        }
    }
}
