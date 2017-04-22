using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFloor : MonoBehaviour
{


	public AnimationCurve anim;
	public float time = 0f;
	public float rotationTime = 0f;
	public int numberRotations;

	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Quaternion.identity;
		transform.Rotate (new Vector3 (-90f, 360f * numberRotations * anim.Evaluate (time / rotationTime)));
		time += Time.deltaTime;

		if (time > rotationTime) {
			time = 0f;
		}


	}
}
