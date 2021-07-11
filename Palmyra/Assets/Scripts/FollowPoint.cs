using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform pointToFollow;
    public float speed = 1;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToFollow.position, Time.deltaTime * speed);
    }
}
