using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    [SerializeField] Transform pointToFollow;
    [SerializeField] float speed = 1;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToFollow.position, Time.deltaTime * 1);
    }
}
