using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour {

	public float	maxRotationX;
	public float	maxRotationY;
	public float	maxRotationZ;
	public float	speed;

	private Vector3 initRotation;
	private Vector3 newRot;
	private float 	sinSpeed;

	void Start () {

		initRotation = transform.rotation.eulerAngles;
	}

	void Update () {

		sinSpeed = Mathf.Sin (Time.time * speed);
		newRot = new Vector3 (sinSpeed * maxRotationX, sinSpeed * maxRotationY, sinSpeed * maxRotationZ);
		transform.rotation = Quaternion.Euler(initRotation.x + newRot.x, initRotation.y + newRot.y, initRotation.z + newRot.z);
	}
}
