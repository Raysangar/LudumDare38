﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotScript : MonoBehaviour
{

	public float vel;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (new Vector3 (0f, vel * Time.deltaTime, 0f));
	}
}
