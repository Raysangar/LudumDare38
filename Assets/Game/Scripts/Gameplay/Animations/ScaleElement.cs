using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleElement : MonoBehaviour
{

	public AnimationCurve anim;
	public float time = 0f;
	public float scaleTime = 0f;
	public float delayTime = 0f;

	void Start ()
	{
		delayTime = Random.Range (0f, 0.3f);
		time = -delayTime * scaleTime;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.localScale = anim.Evaluate (time) * Vector3.one;
		time += Time.deltaTime;

		if (time > scaleTime - delayTime * scaleTime) {
			time = -delayTime * scaleTime;
		}


	}
}
