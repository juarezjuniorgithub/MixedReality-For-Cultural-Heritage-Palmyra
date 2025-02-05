﻿using UnityEngine;
using System.Collections;
using Photon.Pun;

/// <summary>
/// Spin the object at a specified speed
/// </summary>
public class SpinFree : MonoBehaviour {
	PhotonView photonview;
	[Tooltip("Spin: Yes or No")]
	public bool spin;
	[Tooltip("Spin the parent object instead of the object this script is attached to")]
	public bool spinParent;
	public float speed = 10f;

	[HideInInspector]
	public bool clockwise = true;
	[HideInInspector]
	public float direction = 1f;
	[HideInInspector]
	public float directionChangeSpeed = 2f;

    private void Start()
    {
		photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update() {
        if (photonview.IsMine)
        {
			if (direction < 1f)
			{
				direction += Time.deltaTime / (directionChangeSpeed / 2);
			}

			if (spin)
			{
				if (clockwise)
				{
					if (spinParent)
						transform.parent.transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
					else
						transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
				}
				else
				{
					if (spinParent)
						transform.parent.transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
					else
						transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
				}
			}
		}

	}
		
}